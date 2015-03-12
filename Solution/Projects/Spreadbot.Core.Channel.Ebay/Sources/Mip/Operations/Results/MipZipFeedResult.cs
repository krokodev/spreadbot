using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class MipZipFeedResult : MipResponseResult
    {
        public MipZipFeedResult(string zipFileName)
        {
            ZipFileName = zipFileName;
        }

        public string ZipFileName { get; set; }

        public override string GetAutoinfo()
        {
            return Template.SafeFormat("ZipFileName", ZipFileName);
        }
    }
}