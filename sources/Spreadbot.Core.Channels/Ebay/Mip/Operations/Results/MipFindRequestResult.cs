// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipFindRequestResult.cs
// Roman, 2015-04-03 1:45 PM

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public class MipFindRequestResult : AbstractMipResponseResult
    {
        public string RemoteFileName { get; set; }
        public string RemoteDir { get; set; }
    }
}