// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsRequestHandler.cs

using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Request
{
    public class MwsRequestHandler
    {
        public MwsRequestHandler( MwsFeedHandler mwsFeedHandler, string requestId )
        {
            MwsFeedHandler = mwsFeedHandler;
            Id = requestId;
        }

        public string Id { get; set; }
        public MwsFeedHandler MwsFeedHandler { get; set; }
    }
}