using System;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.System
{
    public interface IChannel
    {
        Guid Id { get;}
        string Name { get; }
        IResponse Publish(IChannelTaskArgs args);
    }
}
