// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayPublishResult.cs
// romak_000, 2015-03-23 16:23

using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;

namespace Spreadbot.Core.Channels.Ebay.Operations.Results
{
    public class EbayPublishResult : AbstractMipResponseResult
    {
        public string MipRequestId { get; set; }

        public override string Autoinfo
        {
            get { return string.Format( Template, "MipRequestId", MipRequestId ); }
        }

        public string MipItemId { get; set; }
    }
}