// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipSftpSendFilesResult.cs

using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results
{
    public class MipSftpSendFilesResult : ResponseResult
    {
        public string LocalFiles { get; set; }
        public string RemoteFiles { get; set; }
    }
}