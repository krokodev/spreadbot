﻿using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.System
{
    public abstract class ChannelTask : Task, IChannelTask
    {
        // ===================================================================================== []
        // Constructor
        protected ChannelTask(ChannelMethod method)
        {
            Method = method;
        }

        // ===================================================================================== []
        // ITask
        public override string Autoinfo
        {
            get
            {
                return base.Autoinfo+" Channel: [{0}] Args: [{1}] Response: [{2}]"
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
    }
}