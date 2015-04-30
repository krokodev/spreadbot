// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipFindRequestResult.cs

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results
{
    public class MipFindRequestResult : AbstractMipResponseResult
    {
        public string RemoteFileName { get; set; }
        public string RemoteDir { get; set; }
    }
}