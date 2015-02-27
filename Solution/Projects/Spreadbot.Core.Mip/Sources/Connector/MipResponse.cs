using System.Globalization;

namespace Spreadbot.Core.Mip
{
    public class MipResponse
    {
        public MipResponse(MipStatusCode statusCode = MipStatusCode.Unknown, string statusDescription="")
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
        }
        public MipStatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }
    }
}