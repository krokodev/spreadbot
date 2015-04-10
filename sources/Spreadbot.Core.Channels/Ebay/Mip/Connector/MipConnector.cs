// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.cs

using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Mip.SftpHelper;
using Spreadbot.Core.Channels.Ebay.Mip.ZipHelper;

namespace Spreadbot.Core.Channels.Ebay.Mip.Connector
{
    public partial class MipConnector : IMipConnector
    {
        // --------------------------------------------------------[]
        public const string MipQueueDepthErrorMessage = "Exceeded the Queue Depth";
        public const string MipWriteToLocationErrorMessage = "You are not eligible to write at this location";

        // --------------------------------------------------------[]
        public MipConnector()
        {
            SftpHelper = new WinScpSftpHelper();
            ZipHelper = new SystemIoZipHelper();
        }

        private static MipConnector _instance;

        public static MipConnector Instance
        {
            get { return _instance ?? ( _instance = new MipConnector() ); }
        }

        // --------------------------------------------------------[]
        public ISftpHelper SftpHelper { get; set; }
        public IZipHelper ZipHelper { get; set; }

        // --------------------------------------------------------[]
        public virtual MipResponse< MipSendFeedResult > SendFeed( MipFeedHandler mipFeedHandler )
        {
            return SendFeed( mipFeedHandler, MipRequestHandler.GenerateId() );
        }

        // --------------------------------------------------------[]
        public MipResponse< MipSendFeedResult > SendFeed( MipFeedHandler mipFeedHandler, string reqId )
        {
            return _SendFeed( mipFeedHandler, reqId );
        }

        // --------------------------------------------------------[]
        public MipResponse< MipFindRequestResult > FindRequest(
            MipRequestHandler mipRequestHandler,
            MipRequestProcessingStage stage )
        {
            return _FindRequest( mipRequestHandler, stage );
        }

        // --------------------------------------------------------[]
        public MipRequestStatusResponse GetRequestStatus( MipRequestHandler mipRequestHandler )
        {
            return _GetRequestStatus( mipRequestHandler );
        }

        // --------------------------------------------------------[]
        public static string LocalFeedXmlFilePath( MipFeedHandler mipFeedHandler )
        {
            return _LocalFeedXmlFilePath( mipFeedHandler );
        }

        // --------------------------------------------------------[]
        public static string LocalFeedFolder( MipFeedHandler mipFeedHandler )
        {
            return _LocalFeedFolder( mipFeedHandler.GetName() );
        }
    }
}