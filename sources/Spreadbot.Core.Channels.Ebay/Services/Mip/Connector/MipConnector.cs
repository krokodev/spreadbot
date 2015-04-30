// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipConnector.cs

using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Services.Mip.SftpHelper;
using Spreadbot.Core.Channels.Ebay.Services.Mip.ZipHelper;

// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Connector
{
    public partial class MipConnector : IMipConnector
    {
        // --------------------------------------------------------[]
        public const string MipQueueDepthErrorMessage = "Exceeded the Queue Depth";
        public const string MipWriteToLocationErrorMessage = "You are not eligible to write at this location";
        private static MipConnector _instance;
        private static readonly object Locker = new object();

        // --------------------------------------------------------[]
        public MipConnector()
        {
            SftpHelper = new WinScpSftpHelper();
            ZipHelper = new SystemIoZipHelper();
        }

        public static MipConnector Instance
        {
            get
            {
                lock( Locker ) {
                    return _instance ?? ( _instance = new MipConnector() );
                }
            }
        }

        // --------------------------------------------------------[]
        public ISftpHelper SftpHelper { get; set; }
        public IZipHelper ZipHelper { get; set; }

        // --------------------------------------------------------[]
        public virtual MipResponse< MipSubmitFeedResult > SubmitFeed( MipFeedHandler mipFeedHandler )
        {
            return SubmitFeed( mipFeedHandler, MipRequestHandler.GenerateId() );
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
        public MipResponse< MipSubmitFeedResult > SubmitFeed( MipFeedHandler mipFeedHandler, string reqId )
        {
            return _SubmitFeed( mipFeedHandler, reqId );
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