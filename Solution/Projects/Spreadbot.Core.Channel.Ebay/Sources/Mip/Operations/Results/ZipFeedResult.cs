using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class ZipFeedResult : IResponseResult
    {
        public ZipFeedResult(string zipFileName)
        {
            ZipFileName = zipFileName;
        }

        public string ZipFileName { get; set; }

        public string GetAutoinfo(string format)
        {
            return format.SafeFormat("ZipFileName", ZipFileName);
        }
    }
}