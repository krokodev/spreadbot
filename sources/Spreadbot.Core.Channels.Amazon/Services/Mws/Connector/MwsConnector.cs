// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.cs

using System.Diagnostics.CodeAnalysis;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Results;
using Spreadbot.Sdk.Common.Operations.Responses;

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

        public virtual Response< MwsSubmitFeedResult > SubmitFeed( MwsFeedDescriptor feedDescriptor )
        {
            return _SubmitFeed( feedDescriptor );
        }

        public virtual Response< MwsGetFeedSubmissionsResult > GetFeedSubmissions()
        {
            return _GetFeedSubmissions();
        }

        public Response< MwsGetFeedSubmissionCountResult > GetFeedSubmissionCount()
        {
            return _GetFeedSubmissionCount();
        }
    }
}