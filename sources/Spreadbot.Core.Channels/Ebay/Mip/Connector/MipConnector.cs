// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.cs
// Roman, 2015-04-03 1:44 PM

using System;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Sdk.Common.Crocodev.Common;
using Spreadbot.Sdk.Common.Exceptions;

// >> Core | Connector

namespace Spreadbot.Core.Channels.Ebay.Mip.Connector
{
    public partial class MipConnector
    {
        public const string MipQueueDepthErrorMessage = "Exceeded the Queue Depth";
        public const string MipWriteToLocationErrorMessage = "You are not eligible to write at this location";

        // ===================================================================================== []
        // SendFeedFolder
        public static MipResponse< MipSendZippedFeedFolderResult > SendZippedFeedFolder( MipFeedHandler mipFeedHandler )
        {
            var reqId = MipRequestHandler.GenerateId();
            return DoSendZippedFeedFolder( mipFeedHandler, reqId );
        }

        // --------------------------------------------------------[]
        public static MipResponse< MipSendZippedFeedFolderResult > SendTestFeedFolder( MipFeedHandler mipFeedHandler )
        {
            var reqId = MipRequestHandler.GenerateTestId();
            return DoSendZippedFeedFolder( mipFeedHandler, reqId );
        }

        // --------------------------------------------------------[]
        private static MipResponse< MipSendZippedFeedFolderResult > DoSendZippedFeedFolder(
            MipFeedHandler mipFeedHandler,
            string reqId )
        {
            MipResponse< MipZipFeedResult > zipResponse;
            MipResponse< MipSftpSendFilesResult > sendResponse;

            try {
                var localFiles = LocalZippedFeedFile( mipFeedHandler.GetName(), reqId );
                var remoteFiles = RemoteFeedOutgoingZipFilePath( mipFeedHandler.GetName(), reqId );

                zipResponse = ZipHelper.ZipFeed( mipFeedHandler, reqId );
                zipResponse.Check();

                sendResponse = SftpHelper.SendFiles( localFiles, remoteFiles );
                sendResponse.Check();
            }
            catch( Exception exception ) {
                return new MipResponse< MipSendZippedFeedFolderResult >( exception ) {
                    StatusCode = MipOperationStatus.SendZippedFeedFolderFailure,
                };
            }

            // Ref: Use array of responses instead of the inner-inner chain
            sendResponse.InnerResponse = zipResponse;
            return new MipResponse< MipSendZippedFeedFolderResult > {
                StatusCode = MipOperationStatus.SendZippedFeedFolderSuccess,
                Result = new MipSendZippedFeedFolderResult { MipRequestId = reqId },
                InnerResponse = sendResponse
            };
        }

        // ===================================================================================== []
        // FindRequest
        public static MipResponse< MipFindRequestResult > FindRequest(
            MipRequestHandler mipRequestHandler,
            MipRequestProcessingStage stage )
        {
            MipResponse< MipFindRemoteFileResult > findResponse;
            try {
                switch( stage ) {
                    case MipRequestProcessingStage.Inprocess :
                        findResponse = FindRequestRemoteFileNameInInprocess( mipRequestHandler );
                        break;
                    case MipRequestProcessingStage.Output :
                        findResponse = FindRequestRemoteFileNameInOutput( mipRequestHandler );
                        break;
                    default :
                        throw new SpreadbotException( "Wrong stage {0}", stage );
                }
                findResponse.Check();
            }
            catch( Exception exception ) {
                return new MipResponse< MipFindRequestResult >( exception ) {
                    StatusCode = MipOperationStatus.FindRequestFailure
                };
            }
            return new MipResponse< MipFindRequestResult > {
                StatusCode = MipOperationStatus.FindRequestSuccess,
                Result =
                    new MipFindRequestResult {
                        RemoteDir = findResponse.Result.RemoteDir,
                        RemoteFileName = findResponse.Result.RemoteFileName
                    },
                InnerResponse = findResponse
            };
        }

        // --------------------------------------------------------[]
        private static MipResponse< MipFindRemoteFileResult > FindRequestRemoteFileNameInInprocess(
            MipRequestHandler mipRequestHandler )
        {
            var remoteDir = RemoteFeedInprocessFolderPath( mipRequestHandler.MipFeedHandler.GetName() );
            var prefix = mipRequestHandler.FileNamePrefix();

            return SftpHelper.FindRemoteFile( prefix, remoteDir );
        }

        // --------------------------------------------------------[]
        public static MipResponse< MipFindRemoteFileResult > FindRequestRemoteFileNameInOutput(
            MipRequestHandler mipRequestHandler )
        {
            var remoteDirs = RemoteFeedOutputFolderPathes( mipRequestHandler.MipFeedHandler.GetName() );
            var prefix = mipRequestHandler.FileNamePrefix();

            return SftpHelper.FindRemoteFile( prefix, remoteDirs );
        }

        // ===================================================================================== []
        // GetRequestStatus
        public static MipRequestStatusResponse GetRequestStatus(
            MipRequestHandler mipRequestHandler,
            bool ignoreInprocess = false )
        {
            try {
                var response = FindRequest( mipRequestHandler, MipRequestProcessingStage.Inprocess );
                if( response.StatusCode == MipOperationStatus.FindRequestSuccess && !ignoreInprocess ) {
                    return new MipRequestStatusResponse {
                        StatusCode = MipOperationStatus.GetRequestStatusSuccess,
                        ArgsInfo = YamlUtils.MakeArgsInfo( mipRequestHandler ),
                        Result = new MipGetRequestStatusResult { MipRequestStatusCode = MipRequestStatus.Inprocess }
                    };
                }

                response = FindRequest( mipRequestHandler, MipRequestProcessingStage.Output );
                if( response.StatusCode == MipOperationStatus.FindRequestSuccess ) {
                    return GetRequestOutputStatus( mipRequestHandler.MipFeedHandler.Type, response );
                }

                return new MipRequestStatusResponse {
                    StatusCode = MipOperationStatus.GetRequestStatusSuccess,
                    ArgsInfo = YamlUtils.MakeArgsInfo( mipRequestHandler ),
                    Result = new MipGetRequestStatusResult { MipRequestStatusCode = MipRequestStatus.Unknown },
                    InnerResponse = response
                };
            }
            catch( Exception exception ) {
                return new MipRequestStatusResponse( exception ) {
                    StatusCode = MipOperationStatus.GetRequestStatusFailure,
                    ArgsInfo = YamlUtils.MakeArgsInfo( mipRequestHandler )
                };
            }
        }

        // --------------------------------------------------------[]
        private static MipRequestStatusResponse GetRequestOutputStatus(
            MipFeedType feedType,
            MipResponse< MipFindRequestResult > response )
        {
            return new MipRequestStatusResponse {
                StatusCode = MipOperationStatus.GetRequestStatusSuccess,
                Result = ReadRequestOutputStatus( feedType, response )
            };
        }

        // --------------------------------------------------------[]
        private static MipGetRequestStatusResult ReadRequestOutputStatus(
            MipFeedType feedType,
            MipResponse< MipFindRequestResult > response )
        {
            var fileName = response.Result.RemoteFileName;
            var remotePath = response.Result.RemoteDir;
            var localPath = LocalRequestResultsFolder();
            var content = SftpHelper.GetRemoteFileContent( remotePath, fileName, localPath );
            return MakeRequestStatusResultByParsingXmlContent( feedType, content );
        }

        // --------------------------------------------------------[]
        public static string LocalFeedXmlFilePath( MipFeedHandler mipFeedHandler )
        {
            return DoLocalFeedXmlFilePath( mipFeedHandler );
        }

        // --------------------------------------------------------[]
        public static string LocalFeedFolder( MipFeedHandler mipFeedHandler )
        {
            return DoLocalFeedFolder( mipFeedHandler.GetName() );
        }
    }
}