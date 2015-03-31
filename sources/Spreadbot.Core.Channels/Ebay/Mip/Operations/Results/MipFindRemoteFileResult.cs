// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipFindRemoteFileResult.cs
// romak_000, 2015-03-26 19:42

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public class MipFindRemoteFileResult : AbstractMipResponseResult
    {
        public string RemoteFileName { get; set; }
        public string RemoteFolderPath { get; set; }
    }
}