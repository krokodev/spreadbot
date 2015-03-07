using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class GetRequestStatusResult: IResponseResult
    {
        public readonly RequetStatus Status;
        public readonly string Description;

        public GetRequestStatusResult(RequetStatus status, string description="")
        {
            Status = status;
            Description = description;
        }

        public string GetDescription(string format)
        {
            return format.SafeFormat("Status", Status) + " " + format.SafeFormat("Description", Description);
        }
    }
}