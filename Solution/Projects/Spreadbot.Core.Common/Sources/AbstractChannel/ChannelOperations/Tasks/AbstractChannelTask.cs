using Crocodev.Common;
using Spreadbot.Sdk.Common;

// !>> Core | ChannelTask

namespace Spreadbot.Core.Common
{
    public abstract class AbstractChannelTask : AbstractTask, IChannelTask
    {
        // ===================================================================================== []
        // Constructor
        protected AbstractChannelTask(ChannelMethod method)
        {
            Method = method;
        }

        // ===================================================================================== []
        // ITask
        public override string Autoinfo
        {
            get
            {
                return base.Autoinfo + " Channel: [{0}] Args: [{1}] Response: [{2}]"
                    .SafeFormat(
                        Channel.Name,
                        Args,
                        Response == null ? "no" : Response.Autoinfo
                    );
            }
        }

        // ===================================================================================== []
        // IChannelTask
        public IChannel Channel { get; protected set; }
        // --------------------------------------------------------[]
        public ChannelMethod Method { get; set; }
        // --------------------------------------------------------[]
        public IChannelTaskArgs ChannelArgs
        {
            get { return (IChannelTaskArgs) Args; }
        }
    }
}