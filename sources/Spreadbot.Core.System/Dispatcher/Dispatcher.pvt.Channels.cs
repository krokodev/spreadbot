// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.System
// Dispatcher.pvt.Channels.cs

using System.Collections.Generic;
using Spreadbot.Core.Abstracts.Channel.Manager;

namespace Spreadbot.Core.System.Dispatcher
{
    public partial class Dispatcher
    {
        private readonly List< IChannelManager > _channels = new List< IChannelManager >();

        // --------------------------------------------------------[]
        private void RegisterChannel( IChannelManager channelManager )
        {
            _channels.Add( channelManager );
        }

        private IChannelManager FindChannel( string channelId )
        {
            return _channels.Find( c => c.Id == channelId );
        }
    }
}