// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipSftpSendFilesResult.cs
// Roman, 2015-04-01 9:10 PM

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public class MipSftpSendFilesResult : AbstractMipResponseResult
    {
        public string LocalFiles { get; set; }
        public string RemoteFiles { get; set; }
    }
}