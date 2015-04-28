﻿// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// EbayChannelManager.imp.IChannelManager.cs

using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;

namespace Spreadbot.Core.Channels.Ebay.Manager
{
    public partial class EbayChannelManager
    {
        // --------------------------------------------------------[]
        public override void RunSubmissionTask( IChannelTask task )
        {
            DoRunSubmissionTask( ( EbaySubmissionTask ) task );
        }

        // --------------------------------------------------------[]
        public override void ProceedTask( IChannelTask channelTask )
        {
            DoProceedTask( ( EbaySubmissionTask ) channelTask );
        }

        // --------------------------------------------------------[]
        public override string Id
        {
            get { return ConstId; }
        }
    }
}