// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.System
// Dispatcher.pvt.Channels.cs
// Roman, 2015-04-01 9:09 PM

using System.Collections.Generic;
using Spreadbot.Core.Abstracts.Channel.Manager;

namespace Spreadbot.Core.System.Dispatcher
{
    public partial class Dispatcher
    {
        // ===================================================================================== []
        // RegisterChannel
        private readonly List< IChannelManager > _channels = new List< IChannelManager >();

        // --------------------------------------------------------[]
        private void RegisterChannel( IChannelManager channelManager )
        {
            _channels.Add( channelManager );
        }

        // ===================================================================================== []
        // FindChannel
        private IChannelManager FindChannel( string channelId )
        {
            return _channels.Find( c => c.Id == channelId );
        }
    }
}