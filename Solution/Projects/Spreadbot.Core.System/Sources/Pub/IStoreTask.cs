using System;
using Crocodev.Common.Identifier;

namespace Spreadbot.Core.System
{
    public interface IStoreTask
    {
        GenericIdentifier<IChannel, Guid> ChannelId { get; set; }
    }
}