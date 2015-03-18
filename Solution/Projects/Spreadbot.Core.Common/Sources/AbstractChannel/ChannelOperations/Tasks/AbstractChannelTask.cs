using Crocodev.Common;
using Nereal.Serialization;
using Spreadbot.Sdk.Common;

// !>> Core | AbstractChannelTask

namespace Spreadbot.Core.Common
{
    public abstract class AbstractChannelTask : AbstractTask, IChannelTask
    {
        // ===================================================================================== []
        // Constructor
        protected AbstractChannelTask(IChannel channelRef, ChannelMethod method)
        {
            ChannelRef = channelRef;
            Method = method;
        }

        // --------------------------------------------------------[]
        protected AbstractChannelTask()
            :this(null, ChannelMethod.Unknown)
        {
        }

        // ===================================================================================== []
        // Properties
        [NotSerialize]
        protected IChannel ChannelRef { get; set; }

        // ===================================================================================== []
        // ITask
        public override string Autoinfo
        {
            get
            {
                return base.Autoinfo + " Channel: [{0}] Args: [{1}] Response: [{2}]"
                    .SafeFormat(
                        ChannelRef.Name,
                        Args,
                        Response == null ? "no" : Response.Autoinfo
                    );
            }
        }

        // ===================================================================================== []
        // IChannelTask
        [NotSerialize]
        // Code: AbstractChannelTask.Channel
        IChannel IChannelTask.ChannelRef
        {
            get { return ChannelRef;}
        }
        // --------------------------------------------------------[]
        public ChannelMethod Method { get; set; }
        // --------------------------------------------------------[]
        public IChannelTaskArgs ChannelArgs
        {
            get { return (IChannelTaskArgs) Args; }
        }
    }
}