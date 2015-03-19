// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// EbayChannel.cs
// romak_000, 2015-03-19 15:37

using System;
using System.Threading;
using Spreadbot.Core.Common.Channel;

namespace Spreadbot.Core.Channel.Ebay.Channel
{
    public partial class EbayChannel : AbstractChannel
    {
        // ===================================================================================== []
        // Instance
        private static readonly Lazy<EbayChannel> LazyInstance =
            new Lazy<EbayChannel>(CreateInstance, LazyThreadSafetyMode.ExecutionAndPublication);

        // --------------------------------------------------------[]
        private static EbayChannel CreateInstance()
        {
            return new EbayChannel();
        }

        // --------------------------------------------------------[]
        public static EbayChannel Instance
        {
            get { return LazyInstance.Value; }
        }
    }
}