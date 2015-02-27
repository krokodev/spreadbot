using System;

namespace Spreadbot.Core.Mip
{
    public class MipFeed
    {
        public MipFeed(MipFeedType feedType)
        {
            _type = feedType;
        }

        private readonly MipFeedType _type;

        public string Name
        {
            get { return _type.ToString().ToLower(); }
        }
    }
}