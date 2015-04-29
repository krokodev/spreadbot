// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.System
// Dispatcher.pvt.Channels.cs

using System.Collections.Generic;
using System.Linq;
using Spreadbot.Core.Abstracts.Channel.Adapter;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Core.System.Dispatcher
{
    public partial class Dispatcher
    {
        private readonly List< IChannelAdapter > _channels = new List< IChannelAdapter >();

        private void RegisterChannel( IChannelAdapter channelAdapter )
        {
            _channels.Add( channelAdapter );
        }

        private IChannelAdapter FindChannel( string channelId )
        {
            return _channels.Find( c => c.Id == channelId );
        }

        private void AssertChannelRegistered( string channelId )
        {
            if( !_channels.Any( c => c.Id == channelId ) ) {
                throw new SpreadbotException( "Unregistered channel [{0}]", channelId );
            }
        }
    }
}