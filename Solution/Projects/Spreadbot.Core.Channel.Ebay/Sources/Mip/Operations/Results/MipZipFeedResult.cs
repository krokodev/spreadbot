using Crocodev.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
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