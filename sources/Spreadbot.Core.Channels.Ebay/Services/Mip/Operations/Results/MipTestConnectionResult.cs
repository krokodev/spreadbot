// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipTestConnectionResult.cs

using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results
{
    public class MipTestConnectionResult : ResponseResult
    {
        public bool Value { get; set; }
    }
}