// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsGetProductInfoResult.cs

using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Results
{
    public class MwsGetProductInfoResult : ResponseResult {
        public string AsinId { get; set; }
        public string Title { get; set; }
        public string SmallImageUrl { get; set; }
        public string XmlContent { get; set; }
    }
}