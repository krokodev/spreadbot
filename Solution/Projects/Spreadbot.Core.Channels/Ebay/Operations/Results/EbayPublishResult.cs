// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayPublishResult.cs
// romak_000, 2015-03-20 13:56

using System;
using Crocodev.Common.Extensions;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;

namespace Spreadbot.Core.Channels.Ebay.Operations.Results
{
    public class EbayPublishResult : AbstractMipResponseResult
    {
        public readonly Guid MipRequestId;

        public EbayPublishResult() {}

        public EbayPublishResult( Guid mipRequestId )
        {
            MipRequestId = mipRequestId;
        }

        public override string Autoinfo
        {
            get { return Template.SafeFormat( "MipRequestId", MipRequestId ); }
        }
    }
}