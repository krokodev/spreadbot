// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipSubmissionStatusResponse.cs

using System;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Results;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Response
{
    public class MwsGetFeedSubmissionListResponse : MwsResponse< MwsGetFeedSubmissionListResult >
    {
        public MwsGetFeedSubmissionListResponse() {}

        public MwsGetFeedSubmissionListResponse( Exception exception )
            : base( exception ) {}

    }
}