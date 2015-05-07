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
            return "(MipSubmissionId = {0})".SafeFormat( mipFeedSubmissionDescriptor.FeedSubmissionId );
        }

        // --------------------------------------------------------[]
        private Response< MipGetFeedSubmissionOverallStatusResult > GetSubmissionOverallStatusFromOutput(
            MipFeedType feedType,
            Response< MipFindSubmissionResult > response,
            MipFeedSubmissionDescriptor mipFeedSubmissionDescriptor )
        {
            return new Response< MipGetFeedSubmissionOverallStatusResult > {
                ArgsInfo = MakeSubmissionStatusArgsInfo( mipFeedSubmissionDescriptor ),
                Result = ReadSubmissionOverallStatus( feedType, response )
            };
        }

        // --------------------------------------------------------[]
        private MipGetFeedSubmissionOverallStatusResult ReadSubmissionOverallStatus(
            MipFeedType feedType,
            Response< MipFindSubmissionResult > response )
        {
            var fileName = response.Result.RemoteFileName;
            var remotePath = response.Result.RemoteDir;
            var localPath = LocalSubmissionResultsFolder();
            var content = SftpHelper.GetRemoteFileContent( remotePath, fileName, localPath );
            return MakeSubmissionOverallStatusResultByParsingXmlContent( feedType, content );
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
                    case MipFeedSubmissionProcessingStatus.Complete :
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
        private Response< MipGetFeedSubmissionOverallStatusResult > _GetFeedSubmissionOverallStatus(
            MipFeedSubmissionDescriptor mipFeedSubmissionDescriptor )
        {
            try {
                var response = FindSubmission( mipFeedSubmissionDescriptor, MipFeedSubmissionProcessingStatus.InProgress );
                if( response.IsSuccessful ) {
                    return new Response< MipGetFeedSubmissionOverallStatusResult > {
                        ArgsInfo = MakeSubmissionStatusArgsInfo( mipFeedSubmissionDescriptor ),
                        Result =
                            new MipGetFeedSubmissionOverallStatusResult {
                                Status = MipFeedSubmissionOverallStatus.InProgress
                            }
                    };
                }

                response = FindSubmission( mipFeedSubmissionDescriptor, MipFeedSubmissionProcessingStatus.Complete );
                if( response.IsSuccessful ) {
                    return GetSubmissionOverallStatusFromOutput( mipFeedSubmissionDescriptor.MipFeedDescriptor.Type,
                        response,
                        mipFeedSubmissionDescriptor );
                }

                return new Response< MipGetFeedSubmissionOverallStatusResult > {
                    ArgsInfo = MakeSubmissionStatusArgsInfo( mipFeedSubmissionDescriptor ),
                    Result =
                        new MipGetFeedSubmissionOverallStatusResult {
                            Status = MipFeedSubmissionOverallStatus.Unknown
                        },
                    InnerResponses = { response }
                };
            }
            catch( Exception exception ) {
                return new Response< MipGetFeedSubmissionOverallStatusResult >( exception ) {
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