// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipSubmitFeedResponse.cs

using System;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Responses
{
    public class MipSubmitFeedResponse : MipResponse< MipSubmitFeedResult >
    {
        public MipSubmitFeedResponse() {}

        public MipSubmitFeedResponse( Exception exception )
            : base( exception ) {}
    }
}