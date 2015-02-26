namespace Spreadbot.Core.Mip
{
    public class MipResponse
    {
        public MipResponse(MipStatusCode mipStatusCode = MipStatusCode.Unknown)
        {
            StatusCode = mipStatusCode;
        }
        public MipStatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }
    }
}