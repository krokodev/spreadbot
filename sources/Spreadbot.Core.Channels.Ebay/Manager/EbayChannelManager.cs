// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels.Ebay
// EbayChannelManager.cs

using System;
using System.Threading;
using Spreadbot.Core.Abstracts.Channel.Manager;

namespace Spreadbot.Core.Channels.Ebay.Manager
{
    public partial class EbayChannelManager : AbstractChannelManager
    {
        // --------------------------------------------------------[]
        private static readonly Lazy< EbayChannelManager > LazyInstance =
            new Lazy< EbayChannelManager >(
                () => new EbayChannelManager(),
                LazyThreadSafetyMode.ExecutionAndPublication );

        // --------------------------------------------------------[]
        public static EbayChannelManager Instance
        {
            get { return LazyInstance.Value; }
        }

        // --------------------------------------------------------[]
        public const string ConstId = "Ebay";
    }
}