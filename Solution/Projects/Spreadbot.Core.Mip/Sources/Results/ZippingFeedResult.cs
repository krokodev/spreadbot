using Crocodev.Common;

namespace Spreadbot.Core.Mip
{
    public class ZippingFeedResult : IResponseResult
    {
        public ZippingFeedResult(string zipFileName)
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