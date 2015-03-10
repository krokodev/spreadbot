using System;
using System.IO;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;
using Spreadbot.Core.System;

namespace Spreadbot.Core.Channel.Ebay
{
    public class EbayChannel : IChannel
    {
        // ===================================================================================== []
        // Name
        private const string ConstName = "eBay";
        // --------------------------------------------------------[]
        public string Name
        {
            get { return ConstName; }
        }

        // ===================================================================================== []
        // Publish
        // Code: EbayChannel : Publish
        public IResponse Publish(IArgs args)
        {
            // Todo: Use Args.FeedContent
            IResponse mipResponse;
            try
            {
                var publishArgs = (EbayPublishArgs)args;

                SaveFeed(publishArgs.Feed);

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

        // --------------------------------------------------------[]
        private void SaveFeed(Feed feed)
        {
            var fileName = MipConnector.NewFeedXmlFilePath(feed);
            using (var file = File.CreateText(fileName))
            {
                file.Write(feed.Content);
            }
        }
    }
}