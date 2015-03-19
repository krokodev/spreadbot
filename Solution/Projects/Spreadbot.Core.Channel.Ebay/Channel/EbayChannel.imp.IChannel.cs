// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// EbayChannel.imp.IChannel.cs
// romak_000, 2015-03-19 14:29

using System;
using Spreadbot.Core.Channel.Ebay.Channel.Operations.Args;
using Spreadbot.Core.Channel.Ebay.Channel.Operations.Results;
using Spreadbot.Core.Channel.Ebay.Channel.Operations.Tasks;
using Spreadbot.Core.Channel.Ebay.Mip.Connector;
using Spreadbot.Core.Channel.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Common.Channel.Operations.Methods;
using Spreadbot.Core.Common.Channel.Operations.Responses;
using Spreadbot.Core.Common.Channel.Operations.Tasks;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Channel.Ebay.Channel
{
    public partial class EbayChannel
    {
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
        
        // ===================================================================================== []
        // Name
        private const string ConstName = "Ebay";
        // --------------------------------------------------------[]
        public override string Name
        {
            get { return ConstName; }
        }
    }
}