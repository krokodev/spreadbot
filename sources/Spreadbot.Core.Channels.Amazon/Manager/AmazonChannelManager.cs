// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// AmazonChannelManager.cs

using System;
using System.Threading;
using Spreadbot.Core.Abstracts.Channel.Manager;

namespace Spreadbot.Core.Channels.Amazon.Manager
{
    public partial class AmazonChannelManager : AbstractChannelManager
    {
        // --------------------------------------------------------[]
        private static readonly Lazy< AmazonChannelManager > LazyInstance =
            new Lazy< AmazonChannelManager >(
                () => new AmazonChannelManager(),
                LazyThreadSafetyMode.ExecutionAndPublication );

        // --------------------------------------------------------[]
        public static AmazonChannelManager Instance
        {
            get { return LazyInstance.Value; }
        }

        // --------------------------------------------------------[]
        public const string ConstId = "Amazon";
    }
}