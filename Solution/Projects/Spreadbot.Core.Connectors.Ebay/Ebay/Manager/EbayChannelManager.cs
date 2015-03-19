// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayChannelManager.cs
// romak_000, 2015-03-19 19:58

using System;
using System.Threading;
using Spreadbot.Core.Common.Channel;


namespace Spreadbot.Core.Connectors.Ebay.Channel
{
    public partial class EbayChannelManager : AbstractChannelManager
    {
        // ===================================================================================== []
        // Instance
        private static readonly Lazy< EbayChannelManager > LazyInstance =
            new Lazy< EbayChannelManager >( CreateInstance, LazyThreadSafetyMode.ExecutionAndPublication );

        // --------------------------------------------------------[]
        private static EbayChannelManager CreateInstance()
        {
            return new EbayChannelManager();
        }


        // --------------------------------------------------------[]
        public static EbayChannelManager Instance { get { return LazyInstance.Value; } }

        // --------------------------------------------------------[]
        public const string ConstId = "Ebay";
    }
}