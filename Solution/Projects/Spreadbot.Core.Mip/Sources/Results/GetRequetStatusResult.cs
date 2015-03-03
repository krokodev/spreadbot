namespace Spreadbot.Core.Mip
{
    public class GetRequetStatusResult
    {
        public readonly RequetStatus Status;
        public readonly string Description;

        public GetRequetStatusResult(RequetStatus status, string description="")
        {
            Status = status;
            Description = description;
        }
    }
}