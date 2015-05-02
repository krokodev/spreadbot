// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipFindRemoteFileResult.cs

using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Results
{
    public class MipFindRemoteFileResult : ResponseResult
    {
        public string RemoteFileName { get; set; }
        public string RemoteDir { get; set; }
    }
}