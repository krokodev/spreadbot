using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Mip
{
    public class ZipFeedResult : IResponseResult
    {
        public ZipFeedResult(string zipFileName)
        {
            ZipFileName = zipFileName;
        }

        public string ZipFileName { get; set; }

        public string GetDescription(string format)
        {
            return format.SafeFormat("ZipFileName", ZipFileName);
        }
    }
}