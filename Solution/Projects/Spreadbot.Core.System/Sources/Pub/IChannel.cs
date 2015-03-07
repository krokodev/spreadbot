using System;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.System
{
    public interface IChannel
    {
        Guid Id { get;}
        IResponse Publish(IChannelTaskArgs args);
    }
}
