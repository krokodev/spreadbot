// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// MipSendZippedFeedFolderResult.cs
// romak_000, 2015-03-19 15:49

using System;
using Crocodev.Common.Extensions;

namespace Spreadbot.Core.Connectors.Ebay.Mip.Operations.Results
{
    public class MipSendZippedFeedFolderResult : AbstractMipResponseResult
    {
        public MipSendZippedFeedFolderResult()
        {
        }

        public MipSendZippedFeedFolderResult(Guid mipRequestId)
        {
            MipRequestId = mipRequestId;
        }

        public override string Autoinfo
        {
            get { return Template.SafeFormat("RequestId", MipRequestId); }
        }

        public override Guid GetMipRequestId()
        {
            return MipRequestId;
        }

        public Guid MipRequestId { get; set; }
    }
}