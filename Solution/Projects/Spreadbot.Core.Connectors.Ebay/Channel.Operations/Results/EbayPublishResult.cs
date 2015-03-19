// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// EbayPublishResult.cs
// romak_000, 2015-03-19 15:49

using System;
using Crocodev.Common.Extensions;
using Spreadbot.Core.Connectors.Ebay.Mip.Operations.Results;

namespace Spreadbot.Core.Connectors.Ebay.Channel.Operations.Results
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