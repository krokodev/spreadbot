// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipFeedDescriptor.cs

using System;
using YamlDotNet.Serialization;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Feed
{
    public class MipFeedDescriptor
    {
        public MipFeedDescriptor()
        {
            Id = Guid.NewGuid().ToString();
        }

        public MipFeedDescriptor( MipFeedType type )
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