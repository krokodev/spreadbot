// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipConnector.cs

using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.FeedSubmission;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Results;
using Spreadbot.Core.Channels.Ebay.Services.Mip.SftpHelper;
using Spreadbot.Core.Channels.Ebay.Services.Mip.ZipHelper;
using Spreadbot.Sdk.Common.Operations.Responses;

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
        public virtual Response< MipSubmitFeedResult > SubmitFeed( MipFeedDescriptor mipFeedDescriptor )
        {
            return SubmitFeed( mipFeedDescriptor, MipFeedSubmissionDescriptor.GenerateId() );
        }

        // --------------------------------------------------------[]
        public Response< MipFindSubmissionResult > FindSubmission(
            MipFeedSubmissionDescriptor mipFeedSubmissionDescriptor,
            MipFeedSubmissionProcessingStatus processingStatus )
        {
            return _FindSubmission( mipFeedSubmissionDescriptor, processingStatus );
        }

        // --------------------------------------------------------[]
        public Response< MipGetFeedSubmissionStatusResult > GetFeedSubmissionStatus(
            MipFeedSubmissionDescriptor mipFeedSubmissionDescriptor )
        {
            return _GetFeedSubmissionStatus( mipFeedSubmissionDescriptor );
        }

        // --------------------------------------------------------[]
        public Response< MipSubmitFeedResult > SubmitFeed( MipFeedDescriptor mipFeedDescriptor, string reqId )
        {
            return _SubmitFeed( mipFeedDescriptor, reqId );
        }

        // --------------------------------------------------------[]
        public static string LocalFeedXmlFilePath( MipFeedDescriptor mipFeedDescriptor )
        {
            return _LocalFeedXmlFilePath( mipFeedDescriptor );
        }

        // --------------------------------------------------------[]
        public static string LocalFeedFolder( MipFeedDescriptor mipFeedDescriptor )
        {
            return _LocalFeedFolder( mipFeedDescriptor.GetName() );
        }
    }
}