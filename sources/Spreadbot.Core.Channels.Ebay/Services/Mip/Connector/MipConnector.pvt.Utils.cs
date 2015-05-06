// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipConnector.pvt.Utils.cs

using System;
using Krokodev.Common.Extensions;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.FeedSubmission;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Results;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Connector
{
    public partial class MipConnector
    {
        // --------------------------------------------------------[]
        protected Response< MipSubmitFeedResult > _SubmitFeed(
            MipFeedDescriptor mipFeedDescriptor,
            string reqId )
        {
            Response< MipZipFeedResult > zipResponse;
            Response< MipSftpSendFilesResult > sendResponse;

            try {
                var localFiles = LocalZippedFeedFile( mipFeedDescriptor.GetName(), reqId );
                var remoteFiles = RemoteFeedOutgoingZipFilePath( mipFeedDescriptor.GetName(), reqId );

                zipResponse = ZipHelper.ZipFeed( mipFeedDescriptor, reqId );
                zipResponse.Check();

                sendResponse = SftpHelper.SendFiles( localFiles, remoteFiles );
                sendResponse.Check();
            }
            catch( Exception exception ) {
                return new Response< MipSubmitFeedResult >( exception );
            }

            return new Response< MipSubmitFeedResult > {
                Result = new MipSubmitFeedResult { FeedSubmissionId = reqId },
                InnerResponses = { zipResponse, sendResponse }
            };
        }

        // --------------------------------------------------------[]
        private Response< MipFindRemoteFileResult > FindSubmissionInFolder_Inprocess(
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
        private Response< MipGetFeedSubmissionStatusResult > GetSubmissionStatusFromOutput(
            MipFeedType feedType,
            Response< MipFindSubmissionResult > response,
            MipFeedSubmissionDescriptor mipFeedSubmissionDescriptor )
        {
            return new Response< MipGetFeedSubmissionStatusResult > {
                ArgsInfo = MakeSubmissionStatusArgsInfo( mipFeedSubmissionDescriptor ),
                Result = ReadSubmissionOutputStatus( feedType, response )
            };
        }

        // --------------------------------------------------------[]
        private MipGetFeedSubmissionStatusResult ReadSubmissionOutputStatus(
            MipFeedType feedType,
            Response< MipFindSubmissionResult > response )
        {
            var fileName = response.Result.RemoteFileName;
            var remotePath = response.Result.RemoteDir;
            var localPath = LocalSubmissionResultsFolder();
            var content = SftpHelper.GetRemoteFileContent( remotePath, fileName, localPath );
            return MakeSubmissionStatusResultByParsingXmlContent( feedType, content );
        }

        // --------------------------------------------------------[]
        private Response< MipFindSubmissionResult > _FindSubmission(
            MipFeedSubmissionDescriptor mipFeedSubmissionDescriptor,
            MipFeedSubmissionProcessingStatus processingStatus )
        {
            Response< MipFindRemoteFileResult > findResponse;
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
                return new Response< MipFindSubmissionResult >( exception );
            }
            return new Response< MipFindSubmissionResult > {
                Result =
                    new MipFindSubmissionResult {
                        RemoteDir = findResponse.Result.RemoteDir,
                        RemoteFileName = findResponse.Result.RemoteFileName
                    },
                InnerResponses = { findResponse }
            };
        }

        // --------------------------------------------------------[]
        private Response< MipGetFeedSubmissionStatusResult > _GetFeedSubmissionStatus(
            MipFeedSubmissionDescriptor mipFeedSubmissionDescriptor )
        {
            try {
                var response = FindSubmission( mipFeedSubmissionDescriptor, MipFeedSubmissionProcessingStatus.InProgress );
                if( response.IsSuccessful ) {
                    return new Response< MipGetFeedSubmissionStatusResult > {
                        ArgsInfo = MakeSubmissionStatusArgsInfo( mipFeedSubmissionDescriptor ),
                        Result =
                            new MipGetFeedSubmissionStatusResult {
                                MipFeedSubmissionStatus = MipFeedSubmissionStatus.InProgress
                            }
                    };
                }

                response = FindSubmission( mipFeedSubmissionDescriptor, MipFeedSubmissionProcessingStatus.Done );
                if( response.IsSuccessful ) {
                    return GetSubmissionStatusFromOutput( mipFeedSubmissionDescriptor.MipFeedDescriptor.Type,
                        response,
                        mipFeedSubmissionDescriptor );
                }

                return new Response< MipGetFeedSubmissionStatusResult > {
                    ArgsInfo = MakeSubmissionStatusArgsInfo( mipFeedSubmissionDescriptor ),
                    Result =
                        new MipGetFeedSubmissionStatusResult {
                            MipFeedSubmissionStatus = MipFeedSubmissionStatus.Unknown
                        },
                    InnerResponses = { response }
                };
            }
            catch( Exception exception ) {
                return new Response< MipGetFeedSubmissionStatusResult >( exception ) {
                    ArgsInfo = MakeSubmissionStatusArgsInfo( mipFeedSubmissionDescriptor )
                };
            }
        }

        // --------------------------------------------------------[]
        private Response< MipFindRemoteFileResult > FindSubmissionInFolder_Output(
            MipFeedSubmissionDescriptor mipFeedSubmissionDescriptor )
        {
            var remoteDirs = RemoteFeedOutputFolderPathes( mipFeedSubmissionDescriptor.MipFeedDescriptor.GetName() );
            var prefix = mipFeedSubmissionDescriptor.FileNamePrefix();

            return SftpHelper.FindRemoteFile( prefix, remoteDirs );
        }
    }
}