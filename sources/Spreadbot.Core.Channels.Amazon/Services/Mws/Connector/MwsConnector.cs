﻿// Spreadbot (c) 2015 Krokodev
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
        public const string MwsYouExceededYourQuota = "You exceeded your quota";

        public MwsConnector()
        {
            InitMwsClients();
        }

        public static IMwsConnector Api
        {
            get { return GetInstance(); }
        }

        public virtual Response< MwsSubmitFeedResult > SubmitFeed( MwsFeedDescriptor feedDescriptor )
        {
            return _SubmitFeed( feedDescriptor );
        }

        public virtual Response< MwsGetFeedSubmissionListResult > GetFeedSubmissionList(
            MwsSubmittedFeedsFilter filter )
        {
            return _GetFeedSubmissionList( filter );
        }

        public Response< MwsGetFeedSubmissionCountResult > GetFeedSubmissionCount( MwsSubmittedFeedsFilter filter )
        {
            return _GetFeedSubmissionCount( filter );
        }

        public Response< MwsGetFeedSubmissionProcessingStatusResult > GetFeedSubmissionProcessingStatus(
            string feedSubmissionId )
        {
            return _GetFeedSubmissionProcessingStatus( feedSubmissionId );
        }

        public Response< MwsGetFeedSubmissionCompleteStatusResult > GetFeedSubmissionCompleteStatus(
            string feedSubmissionId )
        {
            return _GetFeedSubmissionCompleteStatus( feedSubmissionId );
        }

        public Response< MwsGetFeedSubmissionOverallStatusResult > GetFeedSubmissionOverallStatus(
            string feedSubmissionId )
        {
            return _GetFeedSubmissionOverallStatus( feedSubmissionId );
        }

        public Response< MwsGetProductInfoResult > GetProductInfo( string sku )
        {
            return _GetProductInfo( sku );
        }
    }
}