// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipSftpSendFilesResult.cs

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public class MipSftpSendFilesResult : AbstractMipResponseResult
    {
        public string LocalFiles { get; set; }
        public string RemoteFiles { get; set; }
    }
}