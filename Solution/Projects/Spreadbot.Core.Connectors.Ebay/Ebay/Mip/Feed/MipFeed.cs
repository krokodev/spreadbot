// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipFeed.cs
// romak_000, 2015-03-19 20:09

using System;

namespace Spreadbot.Core.Connectors.Ebay.Mip.Feed
{
    public class MipFeed
    {
        public MipFeed( MipFeedType mipFeedType )
        {
            _type = mipFeedType;
            Id = Guid.NewGuid();
        }

        private readonly MipFeedType _type;

        public string Name { get { return _type.ToString().ToLower(); } }
        public string Content { get; set; }
        public Guid Id { get; private set; }
        public string ItemInfo { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}