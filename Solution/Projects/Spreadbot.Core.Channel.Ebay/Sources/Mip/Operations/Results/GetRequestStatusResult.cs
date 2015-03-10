using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class GetRequestStatusResult: IResponseResult
    {
        public readonly RequetStatus Status;
        public readonly string Autoinfo;

        public GetRequestStatusResult(RequetStatus status, string autoinfo = "")
        {
            Status = status;
            Autoinfo = autoinfo;
        }

        public string GetAutoinfo(string format)
        {
            return format.SafeFormat("Status", Status) + " " + format.SafeFormat("Autoinfo", Autoinfo);
        }
    }
}