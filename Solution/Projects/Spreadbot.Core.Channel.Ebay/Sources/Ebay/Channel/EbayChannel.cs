using System;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;
using Spreadbot.Core.System;

namespace Spreadbot.Core.Channel.Ebay
{
    public class EbayChannel : IChannel
    {
        private const string ConstName = "eBay";

        public string Name
        {
            get { return ConstName; }
        }

        public IResponse Publish(IArgs args)
        {
            // Code: EbayChannel : Publish
            var publishArgs = args as EbayPublishArgs;

            if (publishArgs == null)
                throw new ArgumentException();

            // Todo: Use Args.FeedContent

            return Connector.SendFeed(publishArgs.Feed);
        }
    }
}