using System;
using System.IO;
using MoreLinq;
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
            IResponse mipResponse;
            try
            {
                var publishArgs = (EbayPublishArgs) args;

                CreateFeedFile(publishArgs.Feed);

                mipResponse = MipConnector.SendZippedFeedFolder(publishArgs.Feed);

                EraseFeedFolder(publishArgs.Feed);

                mipResponse.Check();
            }
            catch (Exception exception)
            {
                return new ChannelResponse<EbayPublishResult>(false, ChannelStatusCode.PublishFail, exception);
            }

            return new ChannelResponse<EbayPublishResult>(true,
                ChannelStatusCode.PublishSuccess,
                new EbayPublishResult(mipResponse),
                mipResponse);
        }

        // --------------------------------------------------------[]
        private void CreateFeedFile(Feed feed)
        {
            CreateFeedFolderIfNeed(feed);

            var fileName = MipConnector.LocalFeedXmlFilePath(feed);
            using (var file = File.CreateText(fileName))
            {
                file.Write(feed.Content);
            }
        }

        // --------------------------------------------------------[]
        private static void CreateFeedFolderIfNeed(Feed feed)
        {
            var feedFolder = MipConnector.LocalFeedFolder(feed);
            if (!Directory.Exists(feedFolder))
            {
                Directory.CreateDirectory(feedFolder);
            }
        }

        // --------------------------------------------------------[]
        private static void EraseFeedFolder(Feed feed)
        {
            Directory
                .GetFiles(MipConnector.LocalFeedFolder(feed))
                .ForEach(File.Delete);
        }
    }
}