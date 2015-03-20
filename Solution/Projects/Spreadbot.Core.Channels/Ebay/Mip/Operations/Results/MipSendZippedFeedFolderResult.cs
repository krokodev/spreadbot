// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipSendZippedFeedFolderResult.cs
// romak_000, 2015-03-20 13:56

using System;
using Crocodev.Common.Extensions;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public class MipSendZippedFeedFolderResult : AbstractMipResponseResult
    {
        public override string Autoinfo
        {
            get { return Template.SafeFormat( "RequestId", MipRequestId ); }
        }

        public string MipRequestId { get; set; }
    }
}