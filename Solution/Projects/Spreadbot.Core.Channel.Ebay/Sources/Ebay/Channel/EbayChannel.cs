using System;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;
using Spreadbot.Core.System;

namespace Spreadbot.Core.Channel.Ebay
{
    public class EbayChannel : IChannel
    {
        private static readonly Guid Guid = new Guid("F754E71E-652A-47B0-A1BC-8D74922D25DC");
        public Guid Id
        {
            get
            {
                return Guid;
            }
        }

        public IResponse Publish(IChannelTaskArgs args)
        {
            // Code: EbayChannel.Publish
            var publishArgs = args as EbayPublishTaskArgs;

            if (publishArgs == null) 
                throw new ArgumentException();

            return Connector.SendFeed(publishArgs.Feed);
        }
    }
}