// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipSubmissionStatusResponse.cs

using System;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Results;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Responses
{
    public class MwsSubmitFeedResponse : MwsResponse< MwsSubmitFeedResult >
    {
        public MwsSubmitFeedResponse() {}

        public MwsSubmitFeedResponse( Exception exception )
            : base( exception ) {}

    }
}