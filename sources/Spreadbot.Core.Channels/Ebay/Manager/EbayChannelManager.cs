// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayChannelManager.cs
// Roman, 2015-04-10 1:28 PM

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