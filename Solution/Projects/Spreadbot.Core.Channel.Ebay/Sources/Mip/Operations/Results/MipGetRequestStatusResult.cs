using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class MipGetRequestStatusResult: MipResponseResult
    {
        public readonly MipRequetStatus Status;
        public readonly string Details;

        public MipGetRequestStatusResult(MipRequetStatus status, string details = "")
        {
            Status = status;
            Details = details;
        }

        public override string GetAutoinfo()
        {
            return Template.SafeFormat("Status", Status) + " " + Template.SafeFormat("Details", Details);
        }
    }
}