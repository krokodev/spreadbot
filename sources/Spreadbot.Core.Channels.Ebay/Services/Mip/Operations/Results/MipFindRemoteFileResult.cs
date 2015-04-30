// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipFindRemoteFileResult.cs

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results
{
    public class MipFindRemoteFileResult : AbstractMipResult
    {
        public string RemoteFileName { get; set; }
        public string RemoteDir { get; set; }
    }
}