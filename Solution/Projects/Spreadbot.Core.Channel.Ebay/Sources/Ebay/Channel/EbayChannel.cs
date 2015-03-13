using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
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
        private const string ConstName = "eBay.Mip";
        // --------------------------------------------------------[]
        public override string Name
        {
            get { return ConstName; }
        }

        // ===================================================================================== []
        // Publish
        public override IChannelResponse Publish(IChannelTaskArgs args)
        {
            MipResponse<MipSendZippedFeedFolderResult> mipResponse;
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
                return new ChannelResponse<EbayPublishResult>(false, ChannelResponseStatusCode.PublishFail, exception);
            }

            return new ChannelResponse<EbayPublishResult>(true,
                ChannelResponseStatusCode.PublishSuccess,
                new EbayPublishResult(mipResponse.Result.MipRequestId),
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
        // Proceed
        public override void ProceedTask(IChannelTask channelTask)
        {
            switch (channelTask.Method)
            {
                case ChannelMethod.Publish:
                    ProceedPublishTask((EbayPublishTask) channelTask);
                    break;
                default:
                    throw new SpreadbotException("Unexpected Task Channel Method: [{0}]", channelTask.Method);
            }
        }

        // --------------------------------------------------------[]
        // Code: ** EbayChanel : ProceedPublishTask
        private static void ProceedPublishTask(EbayPublishTask task)
        {
            task.AssertCanBeProceeded();

            MipRequestStatusResponse statusResponse = null;

            try
            {
                var args = (EbayPublishArgs) task.ChannelArgs;
                var request = new MipRequest(args.Feed, task.Response.Result.MipRequestId);

                statusResponse = MipConnector.GetRequestStatus(request);
                statusResponse.Check();

                task.MipRequestStatusCode = statusResponse.Result.MipRequestStatusCode;
                task.SaveProceedInfo(statusResponse);
            }
            catch
            {
                task.MipRequestStatusCode = MipRequestStatus.Fail;
                task.SaveProceedInfo(statusResponse);
            }
        }
    }
}