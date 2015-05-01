// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.cs

using System.Diagnostics.CodeAnalysis;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Response;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Connector
{
    [SuppressMessage( "ReSharper", "ClassWithVirtualMembersNeverInherited.Global" )]
    public partial class MwsConnector : IMwsConnector
    {
        public const string MwsRequestIsThrottled = "Request is throttled";

        public MwsConnector()
        {
            InitServiceClient();
        }

        public static IMwsConnector Instance
        {
            get { return GetInstance(); }
        }

        public virtual MwsSubmitFeedResponse SubmitFeed( MwsFeedDescriptor feedDescriptor )
        {
            return _SubmitFeed( feedDescriptor );
        }

        public virtual MwsGetFeedSubmissionListResponse GetFeedSubmissionList()
        {
            return _GetFeedSubmissionList();
        }
    }
}