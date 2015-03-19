// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// EbayPublishResult.cs
// romak_000, 2015-03-19 14:02

using System;
using Crocodev.Common;
using Spreadbot.Core.Channel.Ebay.Mip.Operations.Results;

namespace Spreadbot.Core.Channel.Ebay.Channel.Operations.Results
{
    public class EbayPublishResult : AbstractMipResponseResult
    {
        public readonly Guid MipRequestId;

        public EbayPublishResult()
        {
        }

        public EbayPublishResult(Guid mipRequestId)
        {
            MipRequestId = mipRequestId;
        }

        public override string Autoinfo
        {
            get { return Template.SafeFormat("MipRequestId", MipRequestId); }
        }
    }
}