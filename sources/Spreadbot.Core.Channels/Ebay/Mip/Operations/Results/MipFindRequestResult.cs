// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipFindRequestResult.cs
// Roman, 2015-04-01 7:54 PM

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public class MipFindRequestResult : AbstractMipResponseResult {
        public string RemoteFileName { get; set; }
        public string RemoteDir { get; set; }
    }
}