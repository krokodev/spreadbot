// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipConnector.pvt.Utils.cs

using System;
using Krokodev.Common.Extensions;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.StatusCode;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Submission;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Connector
{
    public partial class MipConnector
    {
        // --------------------------------------------------------[]
        protected MipSubmitFeedResponse _SubmitFeed(
            MipFeedDescriptor mipFeedDescriptor,
            string reqId )
        {
            MipResponse< MipZipFeedResult > zipResponse;
            MipResponse< MipSftpSendFilesResult > sendResponse;

            try {
                var localFiles = LocalZippedFeedFile( mipFeedDescriptor.GetName(), reqId );
                var remoteFiles = RemoteFeedOutgoingZipFilePath( mipFeedDescriptor.GetName(), reqId );

                zipResponse = ZipHelper.ZipFeed( mipFeedDescriptor, reqId );
                zipResponse.Check();

                sendResponse = SftpHelper.SendFiles( localFiles, remoteFiles );
                sendResponse.Check();
            }
            catch( Exception exception ) {
                return new MipSubmitFeedResponse ( exception ) {
                    StatusCode = MipOperationStatus.SubmitFeedFailure,
                };
            }

            return new MipSubmitFeedResponse {
                StatusCode = MipOperationStatus.SubmitFeedSuccess,
                Result = new MipSubmitFeedResult { FeedSubmissionId = reqId },
                InnerResponses = { zipResponse, sendResponse }
            };
        }

        // --------------------------------------------------------[]
        private MipResponse< MipFindRemoteFileResult > FindSubmissionIn_Inprocess(
            MipSubmissionDescriptor mipSubmissionDescriptor )
        {
            var remoteDir = RemoteFeedInprocessFolderPath( mipSubmissionDescriptor.MipFeedDescriptor.GetName() );
            var prefix = mipSubmissionDescriptor.FileNamePrefix();

            return SftpHelper.FindRemoteFile( prefix, remoteDir );
        }

        // --------------------------------------------------------[]
        private static string MakeSubmissionStatusArgsInfo( MipSubmissionDescriptor mipSubmissionDescriptor )
        {
            return "(MipSubmissionId = {0})".SafeFormat( mipSubmissionDescriptor.SubmissionId );
        }

        // --------------------------------------------------------[]
        private MipSubmissionStatusResponse GetSubmissionStatusFromOutput(
            MipFeedType feedType,
            MipResponse< MipFindSubmissionResult > response,
            MipSubmissionDescriptor mipSubmissionDescriptor )
        {
            return new MipSubmissionStatusResponse {
                StatusCode = MipOperationStatus.GetSubmissionStatusSuccess,
                ArgsInfo = MakeSubmissionStatusArgsInfo( mipSubmissionDescriptor ),
                Result = ReadSubmissionOutputStatus( feedType, response )
            };
        }

        // --------------------------------------------------------[]
        private MipGetSubmissionStatusResult ReadSubmissionOutputStatus(
            MipFeedType feedType,
            MipResponse< MipFindSubmissionResult > response )
        {
            var fileName = response.Result.RemoteFileName;
            var remotePath = response.Result.RemoteDir;
            var localPath = LocalSubmissionResultsFolder();
            var content = SftpHelper.GetRemoteFileContent( remotePath, fileName, localPath );
            return MakeSubmissionStatusResultByParsingXmlContent( feedType, content );
        }

        // --------------------------------------------------------[]
        private MipResponse< MipFindSubmissionResult > _FindSubmission(
            MipSubmissionDescriptor mipSubmissionDescriptor,
            MipSubmissionStage stage )
        {
            MipResponse< MipFindRemoteFileResult > findResponse;
            try {
                switch( stage ) {
                    case MipSubmissionStage.Inprocess :
                        findResponse = FindSubmissionIn_Inprocess( mipSubmissionDescriptor );
                        break;
                    case MipSubmissionStage.Output :
                        findResponse = FindSubmissionIn_Output( mipSubmissionDescriptor );
                        break;
                    default :
                        throw new SpreadbotException( "Wrong stage {0}", stage );
                }
                findResponse.Check();
            }
            catch( Exception exception ) {
                return new MipResponse< MipFindSubmissionResult >( exception ) {
                    StatusCode = MipOperationStatus.FindSubmissionFailure
                };
            }
            return new MipResponse< MipFindSubmissionResult > {
                StatusCode = MipOperationStatus.FindSubmissionSuccess,
                Result =
                    new MipFindSubmissionResult {
                        RemoteDir = findResponse.Result.RemoteDir,
                        RemoteFileName = findResponse.Result.RemoteFileName
                    },
                InnerResponses = { findResponse }
            };
        }

        // --------------------------------------------------------[]
        private MipSubmissionStatusResponse _GetSubmissionStatus( MipSubmissionDescriptor mipSubmissionDescriptor )
        {
            try {
                var response = FindSubmission( mipSubmissionDescriptor, MipSubmissionStage.Inprocess );
                if( response.StatusCode == MipOperationStatus.FindSubmissionSuccess ) {
                    return new MipSubmissionStatusResponse {
                        StatusCode = MipOperationStatus.GetSubmissionStatusSuccess,
                        ArgsInfo = MakeSubmissionStatusArgsInfo( mipSubmissionDescriptor ),
                        Result =
                            new MipGetSubmissionStatusResult { MipSubmissionStatusCode = MipSubmissionStatus.Inprocess }
                    };
                }

                response = FindSubmission( mipSubmissionDescriptor, MipSubmissionStage.Output );
                if( response.StatusCode == MipOperationStatus.FindSubmissionSuccess ) {
                    return GetSubmissionStatusFromOutput( mipSubmissionDescriptor.MipFeedDescriptor.Type,
                        response,
                        mipSubmissionDescriptor );
                }

                return new MipSubmissionStatusResponse {
                    StatusCode = MipOperationStatus.GetSubmissionStatusSuccess,
                    ArgsInfo = MakeSubmissionStatusArgsInfo( mipSubmissionDescriptor ),
                    Result = new MipGetSubmissionStatusResult { MipSubmissionStatusCode = MipSubmissionStatus.Unknown },
                    InnerResponses = { response }
                };
            }
            catch( Exception exception ) {
                return new MipSubmissionStatusResponse( exception ) {
                    StatusCode = MipOperationStatus.GetSubmissionStatusFailure,
                    ArgsInfo = MakeSubmissionStatusArgsInfo( mipSubmissionDescriptor )
                };
            }
        }

        // --------------------------------------------------------[]
        private MipResponse< MipFindRemoteFileResult > FindSubmissionIn_Output(
            MipSubmissionDescriptor mipSubmissionDescriptor )
        {
            var remoteDirs = RemoteFeedOutputFolderPathes( mipSubmissionDescriptor.MipFeedDescriptor.GetName() );
            var prefix = mipSubmissionDescriptor.FileNamePrefix();

            return SftpHelper.FindRemoteFile( prefix, remoteDirs );
        }
    }
}