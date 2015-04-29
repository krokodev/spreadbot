// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// EbayAdapter.cs

using System;
using System.Threading;
using Spreadbot.Core.Abstracts.Channel.Adapter;

namespace Spreadbot.Core.Channels.Ebay.Adapter
{
    public partial class EbayAdapter : IChannelAdapter
    {
        // --------------------------------------------------------[]
        private static readonly Lazy< EbayAdapter > LazyInstance =
            new Lazy< EbayAdapter >(
                () => new EbayAdapter(),
                LazyThreadSafetyMode.ExecutionAndPublication );

        // --------------------------------------------------------[]
        public static EbayAdapter Instance
        {
            get { return LazyInstance.Value; }
        }

        // --------------------------------------------------------[]
        public const string ConstId = "Ebay";
    }
}