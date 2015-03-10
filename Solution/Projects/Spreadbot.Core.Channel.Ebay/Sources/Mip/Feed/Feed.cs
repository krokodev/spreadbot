using System;
using Crocodev.Common.Identifier;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class Feed : Identifiable<Feed, Guid>
    {
        public Feed(FeedType feedType)
        {
            _type = feedType;
            Id = (Identifier) Guid.NewGuid();
        }

        private readonly FeedType _type;

        public string Name
        {
            get { return _type.ToString().ToLower(); }
        }

        public string Content { get; set; }
        public Identifier Id { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}