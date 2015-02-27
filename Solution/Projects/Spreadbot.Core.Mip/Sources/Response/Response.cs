namespace Spreadbot.Core.Mip
{
    public class Response
    {
        public Response(StatusCode statusCode = StatusCode.Unknown, string statusDescription="")
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
        }
        public StatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public Request.Identifier RequestId { get; set; }
    }
}