// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// EbayChannel.cs
// romak_000, 2015-03-19 14:02

using System;
using System.IO;
using System.Threading;
using MoreLinq;
using Spreadbot.Core.Channel.Ebay.Channel.Operations.Args;
using Spreadbot.Core.Channel.Ebay.Channel.Operations.Results;
using Spreadbot.Core.Channel.Ebay.Channel.Operations.Tasks;
using Spreadbot.Core.Channel.Ebay.Mip.Connector;
using Spreadbot.Core.Channel.Ebay.Mip.Feed;
using Spreadbot.Core.Channel.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channel.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Common.Channel;
using Spreadbot.Core.Common.Channel.Operations.Methods;
using Spreadbot.Core.Common.Channel.Operations.Responses;
using Spreadbot.Core.Common.Channel.Operations.Tasks;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Channel.Ebay.Channel
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
                var publishTask = (EbayPublishTask) task;
                var publishArgs = (EbayPublishArgs) task.Args;

                CreateFeedFile(publishArgs.Feed);

                var mipResponse = MipConnector.SendZippedFeedFolder(publishArgs.Feed);

                EraseFeedFolder(publishArgs.Feed);

                mipResponse.Check();

                ((ITask) publishTask).Response = new ChannelResponse<EbayPublishResult>(true,
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