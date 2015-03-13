using System;
using System.IO;
using MoreLinq;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;
using Spreadbot.Core.System;
using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.Channel.Ebay
{
    public class EbayChannel : AbstractChannel
    {
        // ===================================================================================== []
        // Name
        private const string ConstName = "eBay";
        // --------------------------------------------------------[]
        public override string Name
        {
            get { return ConstName; }
        }

        // ===================================================================================== []
        // Publish
        // Code: EbayChannel : Publish
        public override IChannelResponse Publish(IChannelTaskArgs args)
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
                return new ChannelResponse<EbayPublishResult>(false, ChannelResponseStatusCode.PublishFail, exception);
            }

            return new ChannelResponse<EbayPublishResult>(true,
                ChannelResponseStatusCode.PublishSuccess,
                new EbayPublishResult(mipResponse.Result.RequestId),
                mipResponse);
        }

        // --------------------------------------------------------[]
        private static void CreateFeedFile(MipFeed mipFeed)
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

        // ===================================================================================== []
        // Update
        public override void Update(IChannelTask channelTask)
        {
            throw new NotImplementedException();
        }
    }
}