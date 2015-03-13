using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.System
{
    public interface IChannel
    {
        string Name { get; }
        IResponse Publish(IArgs args);
    }
}
