// Spreadbot (c) 2015 Krokodev
// Spreadbot.Api
// IChannels.cs

using System.Collections.Generic;

namespace Spreadbot.Api.Channels
{
    public interface IChannelManager
    {
        IList< IChannel > GetChannels();
    }
}