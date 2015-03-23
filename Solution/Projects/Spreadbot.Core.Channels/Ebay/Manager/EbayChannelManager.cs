﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayChannelManager.cs
// romak_000, 2015-03-21 2:11

using System;
using System.Threading;
using Spreadbot.Core.Abstracts.Chanel.Manager;

namespace Spreadbot.Core.Channels.Ebay.Manager
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
        public static EbayChannelManager Instance
        {
            get { return LazyInstance.Value; }
        }

        // --------------------------------------------------------[]
        public const string ConstId = "Ebay";
    }
}