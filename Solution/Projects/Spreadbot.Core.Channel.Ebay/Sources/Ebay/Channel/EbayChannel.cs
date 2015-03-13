using System;
using System.IO;
using MoreLinq;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.System;
using Spreadbot.Sdk.Common;

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
            MipResponse<MipSendZippedFeedFolderResult> mipResponse;
            try
            {
                var publishArgs = (EbayPublishArgs) args;

                CreateFeedFile(publishArgs.MipFeed);

                mipResponse = MipConnector.SendZippedFeedFolder(publishArgs.MipFeed);

                EraseFeedFolder(publishArgs.MipFeed);

                mipResponse.Check();
            }
            catch (Exception exception)
            {
                return new ChannelResponse<EbayPublishResult>(false, ChannelStatusCode.PublishFail, exception);
            }

            return new ChannelResponse<EbayPublishResult>(true,
                ChannelStatusCode.PublishSuccess,
                new EbayPublishResult(mipResponse.Result.RequestId),
                mipResponse);
        }

        // --------------------------------------------------------[]
        private void CreateFeedFile(MipFeed mipFeed)
        {
            CreateFeedFolderIfNeed(mipFeed);

            var fileName = MipConnector.LocalFeedXmlFilePath(mipFeed);
            using (var file = File.CreateText(fileName))
            {
                file.Write(mipFeed.Content);
            }
        }

        // --------------------------------------------------------[]
        private static void CreateFeedFolderIfNeed(MipFeed mipFeed)
        {
            var feedFolder = MipConnector.LocalFeedFolder(mipFeed);
            if (!Directory.Exists(feedFolder))
            {
                Directory.CreateDirectory(feedFolder);
            }
        }

        // --------------------------------------------------------[]
        private static void EraseFeedFolder(MipFeed mipFeed)
        {
            Directory
                .GetFiles(MipConnector.LocalFeedFolder(mipFeed))
                .ForEach(File.Delete);
        }
    }
}