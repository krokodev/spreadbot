namespace Spreadbot.Core.Mip
{
    public class MipResponse
    {
        public MipResponse()
        {
            StatusCode = MipStatusCode.Unknown;
        }
        public MipStatusCode StatusCode { get; set; }
    }
}