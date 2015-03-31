// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipFeedHandler.cs
// Roman, 2015-03-31 1:26 PM

using System;
using YamlDotNet.Serialization;

namespace Spreadbot.Core.Channels.Ebay.Mip.Feed
{
    public class MipFeedHandler
    {
        public MipFeedHandler()
        {
            Id = Guid.NewGuid().ToString();
        }

        public MipFeedHandler( MipFeedType type )
            : this()
        {
            Type = type;
        }

        public string GetName()
        {
            return Type.ToString().ToLower();
        }

        [YamlMember( Alias = "FeedType" )]
        public MipFeedType Type { get; set; }

        [YamlMember( Alias = "FeedId" )]
        public string Id { get; private set; }

        [YamlMember( Order = 90 )]
        public string ItemInfo { get; set; }

        [YamlMember( Alias = "FeedContent", Order = 99 )]
        public string Content { get; set; }

        public override string ToString()
        {
            return GetName();
        }
    }
}