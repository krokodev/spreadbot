using Crocodev.Common;

namespace Spreadbot.Core.Mip
{
    public class GettingRequestStatusResult: IResponseResult
    {
        public readonly RequetStatus Status;
        public readonly string Description;

        public GettingRequestStatusResult(RequetStatus status, string description="")
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