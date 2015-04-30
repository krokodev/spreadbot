// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipSftpSendFilesResult.cs

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results
{
    public class MipSftpSendFilesResult : AbstractMipResult
    {
        public string LocalFiles { get; set; }
        public string RemoteFiles { get; set; }
    }
}