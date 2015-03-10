﻿using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.System
{
    // Code: ChannelTask
    public class ChannelTask : Task, IChannelTask
    {
        // ===================================================================================== []
        // ITask
        public override string Autoinfo
        {
            get
            {
                return "Channel:[{0}] Args:[{1}] Response:[{2}]"
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
    }
}