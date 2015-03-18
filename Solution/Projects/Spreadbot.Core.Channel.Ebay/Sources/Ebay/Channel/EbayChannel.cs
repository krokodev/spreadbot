// !>> EbayChannel
using System;
using System.IO;
using System.Threading;
using MoreLinq;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;
using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.Channel.Ebay
{
    //Todo: Ref: Split file
    public class EbayChannel : AbstractChannel
    {
        // ===================================================================================== []
        // Name
        private const string ConstName = "Ebay";
        // --------------------------------------------------------[]
        public override string Name
        {
            get { return ConstName; }
        }

        // ===================================================================================== []
        // Instance
        private static readonly Lazy<EbayChannel> LazyInstance =
            new Lazy<EbayChannel>(CreateInstance, LazyThreadSafetyMode.ExecutionAndPublication);

        // --------------------------------------------------------[]
        public EbayChannel()
        {
        }

        // --------------------------------------------------------[]
        private static EbayChannel CreateInstance()
        {
            return new EbayChannel();
        }

        // --------------------------------------------------------[]
        public static EbayChannel Instance
        {
            get { return LazyInstance.Value; }
        }


        // ===================================================================================== []
        // Publish
        public override void Publish(IChannelTask task)
        {
            try
            {
                var publishTask = (EbayPublishTask)task;
                var publishArgs = (EbayPublishArgs)task.Args;

                CreateFeedFile(publishArgs.Feed);

                var mipResponse = MipConnector.SendZippedFeedFolder(publishArgs.Feed);

                EraseFeedFolder(publishArgs.Feed);

                mipResponse.Check();

                ((IHierarchicalTask) publishTask).Response = new ChannelResponse<EbayPublishResult>(true,
                    ChannelResponseStatusCode.PublishSuccess,
                    new EbayPublishResult(mipResponse.Result.MipRequestId),
                    mipResponse);

                publishTask.MipRequestStatusCode = MipRequestStatus.Initial;
            }
            catch (Exception exception)
            {
                task.Response = new ChannelResponse<EbayPublishResult>(false, ChannelResponseStatusCode.PublishFail,
                    exception);
            }
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
            switch (channelTask.ChannelMethod)
            {
                case ChannelMethod.Publish:
                    ProceedPublishTask((EbayPublishTask) channelTask);
                    break;
                default:
                    throw new SpreadbotException("Unexpected Task Channel Method: [{0}]", channelTask.ChannelMethod);
            }
        }

        // --------------------------------------------------------[]
        private static void ProceedPublishTask(EbayPublishTask ebayPublishTask)
        {
            ((IProceedableTask) ebayPublishTask).AssertCanBeProceeded();

            MipRequestStatusResponse statusResponse = null;

            try
            {
                var ebayPublishArgs = (EbayPublishArgs) ebayPublishTask.GetChannelArgs();
                var mipRequest = new MipRequest(ebayPublishArgs.Feed, ebayPublishTask.GetMipRequestId());

                statusResponse = MipConnector.GetRequestStatus(mipRequest);
                statusResponse.Check();

                ebayPublishTask.MipRequestStatusCode = statusResponse.Result.MipRequestStatusCode;
                ((IProceedableTask) ebayPublishTask).SaveProceedInfo(statusResponse);
            }
            catch
            {
                ebayPublishTask.MipRequestStatusCode = MipRequestStatus.Fail;
                ((IProceedableTask) ebayPublishTask).SaveProceedInfo(statusResponse);
            }
        }
    }
}