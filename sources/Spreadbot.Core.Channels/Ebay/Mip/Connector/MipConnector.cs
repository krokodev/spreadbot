// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.cs
// Roman, 2015-04-06 3:38 PM

using System;
using System.Threading;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;

namespace Spreadbot.Core.Channels.Ebay.Mip.Connector
{
    public partial class MipConnector  : IMipConnector
    {
        // --------------------------------------------------------[]
        public const string MipQueueDepthErrorMessage = "Exceeded the Queue Depth";
        public const string MipWriteToLocationErrorMessage = "You are not eligible to write at this location";

        // --------------------------------------------------------[]
        private static MipConnector _instance;
        public static MipConnector GetInstance()
        {
            return _instance ?? ( _instance = new MipConnector() );
        }

        // --------------------------------------------------------[]
        public static MipResponse< MipSendFeedResult > SendFeed( MipFeedHandler mipFeedHandler )
        {
            var reqId = MipRequestHandler.GenerateId();
            return DoSendFeed( mipFeedHandler, reqId );
        }

        // --------------------------------------------------------[]
        public static MipResponse< MipSendFeedResult > SendTestFeed( MipFeedHandler mipFeedHandler )
        {
            var reqId = MipRequestHandler.GenerateTestId();
            return DoSendFeed( mipFeedHandler, reqId );
        }

        // --------------------------------------------------------[]
        public static MipResponse< MipFindRequestResult > FindRequest(
            MipRequestHandler mipRequestHandler,
            MipRequestProcessingStage stage )
        {
            return _FindRequest( mipRequestHandler, stage );
        }

        // --------------------------------------------------------[]
        public static MipResponse< MipFindRemoteFileResult > FindRequestIn_Output(
            MipRequestHandler mipRequestHandler )
        {
            var remoteDirs = RemoteFeedOutputFolderPathes( mipRequestHandler.MipFeedHandler.GetName() );
            var prefix = mipRequestHandler.FileNamePrefix();

            return SftpHelper.FindRemoteFile( prefix, remoteDirs );
        }

        // --------------------------------------------------------[]
        public static MipRequestStatusResponse GetRequestStatus( MipRequestHandler mipRequestHandler )
        {
            return _GetRequestStatus( mipRequestHandler );
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