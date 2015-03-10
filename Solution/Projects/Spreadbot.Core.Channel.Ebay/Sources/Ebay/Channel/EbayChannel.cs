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

        // Code: EbayChannel : Publish

        public IResponse Publish(IArgs args)
        {
            // Todo: Use Args.FeedContent
            IResponse mipResponse;
            try
            {
                var publishArgs = args as EbayPublishArgs;
                if (publishArgs == null)
                    throw new ArgumentException();

                //SaveFeed(Args.FeedContent);

                mipResponse = MipConnector.SendFeed(publishArgs.Feed);
                mipResponse.Check();
            }
            catch (Exception exception)
            {
                return new ChannelResponse<BoolResult>(false, ChannelStatusCode.PublishFail, exception);
            }

            return new ChannelResponse<BoolResult>(true,
                ChannelStatusCode.PublishSuccess,
                new BoolResult(true),
                mipResponse);
        }
    }
}