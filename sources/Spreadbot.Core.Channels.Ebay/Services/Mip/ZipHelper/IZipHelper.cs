// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// IZipHelper.cs

using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.ZipHelper
{
    public interface IZipHelper
    {
        Response< MipZipFeedResult > ZipFeed( MipFeedDescriptor mipFeedDescriptor, string reqId );
    }
}