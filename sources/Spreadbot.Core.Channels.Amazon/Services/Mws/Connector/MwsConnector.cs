// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.cs

using System.Diagnostics.CodeAnalysis;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Services.Mws.FeedSubmission;
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

        public virtual Response< MwsGetFeedSubmissionListResult > GetFeedSubmissionList( MwsSubmittedFeedsFilter filter )
        {
            return _GetFeedSubmissionList( filter );
        }

        public Response< MwsGetFeedSubmissionCountResult > GetFeedSubmissionCount( MwsSubmittedFeedsFilter filter )
        {
            return _GetFeedSubmissionCount( filter );
        }

        public Response< MwsGetFeedSubmissionProcessingStatusResult > GetFeedSubmissionProcessingStatus( string feedSubmissionId)
        {
            return _GetFeedSubmissionProcessingStatus( feedSubmissionId );
        }
    }
}