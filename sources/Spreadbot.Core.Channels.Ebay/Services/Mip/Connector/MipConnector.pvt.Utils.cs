// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipConnector.pvt.Utils.cs

using System;
using Krokodev.Common.Extensions;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.FeedSubmission;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Responses;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results;
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
                return new MipSubmitFeedResponse( exception );
            }

            return new MipSubmitFeedResponse {
                Result = new MipSubmitFeedResult { FeedSubmissionId = reqId },
                InnerResponses = { zipResponse, sendResponse }
            };
        }

        // --------------------------------------------------------[]
        private MipResponse< MipFindRemoteFileResult > FindSubmissionInFolder_Inprocess(
            MipFeedSubmissionDescriptor mipFeedSubmissionDescriptor )
        {
            var remoteDir = RemoteFeedInprocessFolderPath( mipFeedSubmissionDescriptor.MipFeedDescriptor.GetName() );
            var prefix = mipFeedSubmissionDescriptor.FileNamePrefix();

            return SftpHelper.FindRemoteFile( prefix, remoteDir );
        }

        // --------------------------------------------------------[]
        private static string MakeSubmissionStatusArgsInfo( MipFeedSubmissionDescriptor mipFeedSubmissionDescriptor )
        {
            return "(MipSubmissionId = {0})".SafeFormat( mipFeedSubmissionDescriptor.SubmissionId );
        }

        // --------------------------------------------------------[]
        private MipSubmissionStatusResponse GetSubmissionStatusFromOutput(
            MipFeedType feedType,
            MipResponse< MipFindSubmissionResult > response,
            MipFeedSubmissionDescriptor mipFeedSubmissionDescriptor )
        {
            return new MipSubmissionStatusResponse {
                ArgsInfo = MakeSubmissionStatusArgsInfo( mipFeedSubmissionDescriptor ),
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
            MipFeedSubmissionDescriptor mipFeedSubmissionDescriptor,
            MipFeedSubmissionProcessingStatus processingStatus )
        {
            MipResponse< MipFindRemoteFileResult > findResponse;
            try {
                switch( processingStatus ) {
                    case MipFeedSubmissionProcessingStatus.InProgress :
                        findResponse = FindSubmissionInFolder_Inprocess( mipFeedSubmissionDescriptor );
                        break;
                    case MipFeedSubmissionProcessingStatus.Done :
                        findResponse = FindSubmissionInFolder_Output( mipFeedSubmissionDescriptor );
                        break;
                    default :
                        throw new SpreadbotException( "Wrong processing status {0}", processingStatus );
                }
                findResponse.Check();
            }
            catch( Exception exception ) {
                return new MipResponse< MipFindSubmissionResult >( exception );
            }
            return new MipResponse< MipFindSubmissionResult > {
                Result =
                    new MipFindSubmissionResult {
                        RemoteDir = findResponse.Result.RemoteDir,
                        RemoteFileName = findResponse.Result.RemoteFileName
                    },
                InnerResponses = { findResponse }
            };
        }

        // --------------------------------------------------------[]
        private MipSubmissionStatusResponse _GetSubmissionStatus(
            MipFeedSubmissionDescriptor mipFeedSubmissionDescriptor )
        {
            try {
                var response = FindSubmission( mipFeedSubmissionDescriptor, MipFeedSubmissionProcessingStatus.InProgress );
                if( response.IsSuccessful ) {
                    return new MipSubmissionStatusResponse {
                        ArgsInfo = MakeSubmissionStatusArgsInfo( mipFeedSubmissionDescriptor ),
                        Result =
                            new MipGetSubmissionStatusResult {
                                MipFeedSubmissionResultStatusCode = MipFeedSubmissionResultStatus.Inprocess
                            }
                    };
                }

                response = FindSubmission( mipFeedSubmissionDescriptor, MipFeedSubmissionProcessingStatus.Done );
                if( response.IsSuccessful ) {
                    return GetSubmissionStatusFromOutput( mipFeedSubmissionDescriptor.MipFeedDescriptor.Type,
                        response,
                        mipFeedSubmissionDescriptor );
                }

                return new MipSubmissionStatusResponse {
                    ArgsInfo = MakeSubmissionStatusArgsInfo( mipFeedSubmissionDescriptor ),
                    Result =
                        new MipGetSubmissionStatusResult {
                            MipFeedSubmissionResultStatusCode = MipFeedSubmissionResultStatus.Unknown
                        },
                    InnerResponses = { response }
                };
            }
            catch( Exception exception ) {
                return new MipSubmissionStatusResponse( exception ) {
                    ArgsInfo = MakeSubmissionStatusArgsInfo( mipFeedSubmissionDescriptor )
                };
            }
        }

        // --------------------------------------------------------[]
        private MipResponse< MipFindRemoteFileResult > FindSubmissionInFolder_Output(
            MipFeedSubmissionDescriptor mipFeedSubmissionDescriptor )
        {
            var remoteDirs = RemoteFeedOutputFolderPathes( mipFeedSubmissionDescriptor.MipFeedDescriptor.GetName() );
            var prefix = mipFeedSubmissionDescriptor.FileNamePrefix();

            return SftpHelper.FindRemoteFile( prefix, remoteDirs );
        }
    }
}