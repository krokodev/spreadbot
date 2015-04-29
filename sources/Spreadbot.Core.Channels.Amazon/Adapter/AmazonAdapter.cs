// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// AmazonAdapter.cs

using System;
using System.Threading;
using Spreadbot.Core.Abstracts.Channel.Adapter;

namespace Spreadbot.Core.Channels.Amazon.Adapter
{
    public partial class AmazonAdapter : IChannelAdapter
    {
        // --------------------------------------------------------[]
        private static readonly Lazy< AmazonAdapter > LazyInstance =
            new Lazy< AmazonAdapter >(
                () => new AmazonAdapter(),
                LazyThreadSafetyMode.ExecutionAndPublication );

        // --------------------------------------------------------[]
        public static AmazonAdapter Instance
        {
            get { return LazyInstance.Value; }
        }

        // --------------------------------------------------------[]
        public const string ConstId = "Amazon";
    }
}