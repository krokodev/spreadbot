﻿// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// IZipHelper.cs

using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.ZipHelper
{
    public interface IZipHelper
    {
        MipResponse< MipZipFeedResult > ZipFeed( MipFeedHandler mipFeedHandler, string reqId );
    }
}