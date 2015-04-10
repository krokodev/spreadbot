// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.pvt.Utils.cs
// Roman, 2015-04-10 1:29 PM

using System;
using Crocodev.Common.Extensions;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Core.Channels.Ebay.Mip.Connector
{
    public partial class MipConnector
    {
        // --------------------------------------------------------[]
        protected MipResponse< MipSendFeedResult > _SendFeed(
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
                return new MipResponse< MipSendFeedResult >( exception ) {
                    StatusCode = MipOperationStatus.SendFeedFailure,
                };
            }

            return new MipResponse< MipSendFeedResult > {
                StatusCode = MipOperationStatus.SendFeedSuccess,
                Result = new MipSendFeedResult { MipRequestId = reqId },
                InnerResponses = { zipResponse, sendResponse }
            };
        }

        // --------------------------------------------------------[]
        private MipResponse< MipFindRemoteFileResult > FindRequestIn_Inprocess(
            MipRequestHandler mipRequestHandler )
        {
            var remoteDir = RemoteFeedInprocessFolderPath( mipRequestHandler.MipFeedHandler.GetName() );
            var prefix = mipRequestHandler.FileNamePrefix();

            return SftpHelper.FindRemoteFile( prefix, remoteDir );
        }

        // --------------------------------------------------------[]
        private static string MakeRequestStatusArgsInfo( MipRequestHandler mipRequestHandler )
        {
            return "(MipRequestId = {0})".SafeFormat( mipRequestHandler.Id );
        }

        // --------------------------------------------------------[]
        private MipRequestStatusResponse GetRequestStatusFromOutput(
            MipFeedType feedType,
            MipResponse< MipFindRequestResult > response,
            MipRequestHandler mipRequestHandler )
        {
            return new MipRequestStatusResponse {
                StatusCode = MipOperationStatus.GetRequestStatusSuccess,
                ArgsInfo = MakeRequestStatusArgsInfo( mipRequestHandler ),
                Result = ReadRequestOutputStatus( feedType, response )
            };
        }

        // --------------------------------------------------------[]
        private MipGetRequestStatusResult ReadRequestOutputStatus(
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
        private MipResponse< MipFindRequestResult > _FindRequest(
            MipRequestHandler mipRequestHandler,
            MipRequestProcessingStage stage )
        {
            MipResponse< MipFindRemoteFileResult > findResponse;
            try {
                switch( stage ) {
                    case MipRequestProcessingStage.Inprocess :
                        findResponse = FindRequestIn_Inprocess( mipRequestHandler );
                        break;
                    case MipRequestProcessingStage.Output :
                        findResponse = FindRequestInOutput( mipRequestHandler );
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
                InnerResponses = { findResponse }
            };
        }

        // --------------------------------------------------------[]
        private MipRequestStatusResponse _GetRequestStatus( MipRequestHandler mipRequestHandler )
        {
            try {
                var response = FindRequest( mipRequestHandler, MipRequestProcessingStage.Inprocess );
                if( response.StatusCode == MipOperationStatus.FindRequestSuccess ) {
                    return new MipRequestStatusResponse {
                        StatusCode = MipOperationStatus.GetRequestStatusSuccess,
                        ArgsInfo = MakeRequestStatusArgsInfo( mipRequestHandler ),
                        Result = new MipGetRequestStatusResult { MipRequestStatusCode = MipRequestStatus.Inprocess }
                    };
                }

                response = FindRequest( mipRequestHandler, MipRequestProcessingStage.Output );
                if( response.StatusCode == MipOperationStatus.FindRequestSuccess ) {
                    return GetRequestStatusFromOutput( mipRequestHandler.MipFeedHandler.Type,
                        response,
                        mipRequestHandler );
                }

                return new MipRequestStatusResponse {
                    StatusCode = MipOperationStatus.GetRequestStatusSuccess,
                    ArgsInfo = MakeRequestStatusArgsInfo( mipRequestHandler ),
                    Result = new MipGetRequestStatusResult { MipRequestStatusCode = MipRequestStatus.Unknown },
                    InnerResponses = { response }
                };
            }
            catch( Exception exception ) {
                return new MipRequestStatusResponse( exception ) {
                    StatusCode = MipOperationStatus.GetRequestStatusFailure,
                    ArgsInfo = MakeRequestStatusArgsInfo( mipRequestHandler )
                };
            }
        }

        // --------------------------------------------------------[]
        private MipResponse< MipFindRemoteFileResult > FindRequestInOutput(
            MipRequestHandler mipRequestHandler )
        {
            var remoteDirs = RemoteFeedOutputFolderPathes( mipRequestHandler.MipFeedHandler.GetName() );
            var prefix = mipRequestHandler.FileNamePrefix();

            return SftpHelper.FindRemoteFile( prefix, remoteDirs );
        }
    }
}