// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipFeedHandler.cs
// romak_000, 2015-03-25 15:24

using System;

namespace Spreadbot.Core.Channels.Ebay.Mip.Feed
{
    public class MipFeedHandler
    {
        public MipFeedHandler()
        {
            Id = Guid.NewGuid().ToString();
        }

        public MipFeedHandler( MipFeedType mipFeedType )
            : this()
        {
            Type = mipFeedType;
        }

        public MipFeedType Type { get; set; }

        public string GetName()
        {
            return Type.ToString().ToLower();
        }

        public string Content { get; set; }
        public string Id { get; private set; }
        public string ItemInfo { get; set; }

        public override string ToString()
        {
            return GetName();
        }
    }
}