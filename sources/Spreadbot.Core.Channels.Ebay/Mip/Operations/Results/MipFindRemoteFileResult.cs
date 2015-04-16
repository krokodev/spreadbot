// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels.Ebay
// MipFindRemoteFileResult.cs

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public class MipFindRemoteFileResult : AbstractMipResponseResult
    {
        public string RemoteFileName { get; set; }
        public string RemoteDir { get; set; }
    }
}