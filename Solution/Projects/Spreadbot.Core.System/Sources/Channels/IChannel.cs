using System;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.System
{
    public interface IChannel
    {
        string Name { get; }
        IResponse Publish(IArgs args);
    }
}
