using System;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class Feed
    {
        public Feed(FeedType feedType)
        {
            _type = feedType;
        }

        private readonly FeedType _type;

        public string Name
        {
            get { return _type.ToString().ToLower(); }
        }
    }
}