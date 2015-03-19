// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// MipFeed.cs
// romak_000, 2015-03-19 15:49

using System;
using Crocodev.Common.Identifier;

namespace Spreadbot.Core.Connectors.Ebay.Mip.Feed
{
    public class MipFeed : Identifiable<MipFeed, Guid>
    {
        public MipFeed(MipFeedType mipFeedType)
        {
            _type = mipFeedType;
            Id = (Identifier) Guid.NewGuid();
        }

        private readonly MipFeedType _type;

        public string Name
        {
            get { return _type.ToString().ToLower(); }
        }

        public string Content { get; set; }
        public Identifier Id { get; set; }
        public string ItemInfo { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}