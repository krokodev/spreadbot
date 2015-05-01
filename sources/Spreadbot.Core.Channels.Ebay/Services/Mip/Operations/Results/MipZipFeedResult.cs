// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipZipFeedResult.cs

using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results
{
    public class MipZipFeedResult : ResponseResult
    {
        public string ZipFileName { get; set; }
    }
}