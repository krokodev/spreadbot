// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.cs
// romak_000, 2015-03-24 11:58

using System;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;

// >> Core | Connector

namespace Spreadbot.Core.Channels.Ebay.Mip.Connector
{
    public partial class MipConnector
    {
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
            try {
                ZipHelper.ZipFeed( mipFeedHandler, reqId ).Check();
                SftpHelper.SendZippedFeed( mipFeedHandler, reqId ).Check();
            }
            catch( Exception exception ) {
                return new MipResponse< MipSendZippedFeedFolderResult >(
                    false,
                    MipStatusCode.SendZippedFeedFolderFail,
                    exception
                    );
            }
            return new MipResponse< MipSendZippedFeedFolderResult >(
                true,
                MipStatusCode.SendZippedFeedFolderSuccess,
                new MipSendZippedFeedFolderResult { MipRequestId = reqId }
                );
        }

        // ===================================================================================== []
        // FindRequest
        public static MipResponse< MipFindRemoteFileResult > FindRequest(
            MipRequestHandler mipRequestHandler,
            MipRequestProcessingStage stage )
        {
            MipResponse< MipFindRemoteFileResult > findResponse;
            try {
                switch( stage ) {
                    case MipRequestProcessingStage.Inprocess :
                        findResponse = SftpHelper.FindRequestRemoteFileNameInInprocess( mipRequestHandler );
                        break;
                    case MipRequestProcessingStage.Output :
                        findResponse = SftpHelper.FindRequestRemoteFileNameInOutput( mipRequestHandler );
                        break;
                    default :
                        throw new Exception( string.Format( "Wrong stage {0}", stage ) );
                }
                findResponse.Check();
            }
            catch( Exception exception ) {
                return new MipResponse< MipFindRemoteFileResult >( false, MipStatusCode.FindRequestFail, exception );
            }
            return new MipResponse< MipFindRemoteFileResult >(
                true,
                MipStatusCode.FindRequestSuccess,
                findResponse.Result,
                findResponse );
        }

        // ===================================================================================== []
        // GetRequestStatus
        public static MipRequestStatusResponse GetRequestStatus(
            MipRequestHandler mipRequestHandler,
            bool ignoreInprocess = false )
        {
            try {
                var response = FindRequest( mipRequestHandler, MipRequestProcessingStage.Inprocess );
                if( response.Code == MipStatusCode.FindRequestSuccess && !ignoreInprocess ) {
                    return new MipRequestStatusResponse(
                        true,
                        MipStatusCode.GetRequestStatusSuccess,
                        new MipGetRequestStatusResult( MipRequestStatus.Inprocess )
                        );
                }

                response = FindRequest( mipRequestHandler, MipRequestProcessingStage.Output );
                if( response.Code == MipStatusCode.FindRequestSuccess ) {
                    return GetRequestOutputStatus(mipRequestHandler.MipFeedHandler.Type, response);
                }

                return new MipRequestStatusResponse(
                    true,
                    MipStatusCode.GetRequestStatusSuccess,
                    new MipGetRequestStatusResult( MipRequestStatus.Unknown ),
                    response
                    );
            }
            catch( Exception exception ) {
                return new MipRequestStatusResponse( false, MipStatusCode.GetRequestStatusFail, exception );
            }
        }

        // --------------------------------------------------------[]
        private static MipRequestStatusResponse GetRequestOutputStatus(
            MipFeedType feedType,
            MipResponse< MipFindRemoteFileResult > response )
        {
            var statusResult = ReadRequestOutputStatus(feedType, response);
            return new MipRequestStatusResponse( true, MipStatusCode.GetRequestStatusSuccess, statusResult );
        }

        // --------------------------------------------------------[]
        private static MipGetRequestStatusResult ReadRequestOutputStatus(
            MipFeedType feedType,
            MipResponse< MipFindRemoteFileResult > response )
        {
            var fileName = response.Result.RemoteFileName;
            var remotePath = response.Result.RemoteFolderPath;
            var localPath = LocalRequestResultsFolder();
            var content = SftpHelper.GetRemoteFileContent( remotePath, fileName, localPath );
            return MakeReqreuesStatusResultByParsingXmlContent(feedType, content);
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
