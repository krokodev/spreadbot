// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.cs

using System.Diagnostics.CodeAnalysis;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Responses;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Results;

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

        public virtual MwsResponse< MwsSubmitFeedResult >  SubmitFeed( MwsFeedDescriptor feedDescriptor )
        {
            return _SubmitFeed( feedDescriptor );
        }

        public virtual MwsResponse< MwsGetFeedSubmissionsResult >  GetFeedSubmissions()
        {
            return _GetFeedSubmissions();
        }

        public MwsResponse< MwsGetFeedSubmissionCountResult >  GetFeedSubmissionCount()
        {
            return _GetFeedSubmissionCount();
        }
    }
}