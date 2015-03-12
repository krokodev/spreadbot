using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class GetRequestStatusResult: ResponseResult
    {
        public readonly RequetStatus Status;
        public readonly string Autoinfo;

        public GetRequestStatusResult(RequetStatus status, string autoinfo = "")
        {
            Status = status;
            Autoinfo = autoinfo;
        }

        public override string GetAutoinfo()
        {
            return Template.SafeFormat("Status", Status) + " " + Template.SafeFormat("Autoinfo", Autoinfo);
        }
    }
}