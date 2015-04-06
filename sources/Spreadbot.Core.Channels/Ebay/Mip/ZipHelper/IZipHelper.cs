// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// IZipHelper.cs
// Roman, 2015-04-06 5:02 PM

using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;

namespace Spreadbot.Core.Channels.Ebay.Mip.ZipHelper
{
    public interface IZipHelper {
        MipResponse< MipZipFeedResult > ZipFeed( MipFeedHandler mipFeedHandler, string reqId );
    }
}