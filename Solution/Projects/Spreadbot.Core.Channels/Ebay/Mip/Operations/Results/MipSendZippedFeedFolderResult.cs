// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipSendZippedFeedFolderResult.cs
// romak_000, 2015-03-21 2:11

using Crocodev.Common.Extensions;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public class MipSendZippedFeedFolderResult : AbstractMipResponseResult
    {
        public override string Autoinfo
        {
            get { return string.Format(Template, "RequestId", MipRequestId); }
        }

        public string MipRequestId { get; set; }
    }
}