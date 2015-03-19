// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// MipZipFeedResult.cs
// romak_000, 2015-03-19 14:02

using Crocodev.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip.Operations.Results
{
    public class MipZipFeedResult : AbstractMipResponseResult
    {
        public MipZipFeedResult(string zipFileName)
        {
            ZipFileName = zipFileName;
        }

        public string ZipFileName { get; set; }

        public override string Autoinfo
        {
            get { return Template.SafeFormat("ZipFileName", ZipFileName); }
        }
    }
}