// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayChannelManager.pvt.Tasks.cs
// romak_000, 2015-03-20 13:56

using System;
using Spreadbot.Core.Abstracts.Chanel.Operations.Methods;
using Spreadbot.Core.Abstracts.Chanel.Operations.Responses;
using Spreadbot.Core.Abstracts.Chanel.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Operations.Args;
using Spreadbot.Core.Channels.Ebay.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Channels.Ebay.Manager
{
    public partial class EbayChannelManager
    {
        // --------------------------------------------------------[]
        private static void ProceedPublishTask( EbayPublishTask ebayPublishTask )
        {
            ( ( IProceedableTask ) ebayPublishTask ).AssertCanBeProceeded();

            MipRequestStatusResponse statusResponse = null;

            try {
                var ebayPublishArgs = ( EbayPublishArgs ) ebayPublishTask.GetChannelArgs();
                var mipRequest = new MipRequest( ebayPublishArgs.Feed, ebayPublishTask.GetMipRequestId() );

                statusResponse = MipConnector.GetRequestStatus( mipRequest );
                statusResponse.Check();

                ebayPublishTask.MipRequestStatusCode = statusResponse.Result.MipRequestStatusCode;
                ( ( IProceedableTask ) ebayPublishTask ).SaveProceedInfo( statusResponse );
            }
            catch {
                ebayPublishTask.MipRequestStatusCode = MipRequestStatus.Fail;
                ( ( IProceedableTask ) ebayPublishTask ).SaveProceedInfo( statusResponse );
            }
        }

        private void DoProceedTask( IChannelTask channelTask )
        {
            switch( channelTask.ChannelMethod ) {
                case ChannelMethod.Publish :
                    ProceedPublishTask( ( EbayPublishTask ) channelTask );
                    break;
                default :
                    throw new SpreadbotException( "Unexpected Task Channel Method: [{0}]", channelTask.ChannelMethod );
            }
        }

        private static void DoRunPublishTask( IChannelTask task )
        {
            try {
                var publishTask = ( EbayPublishTask ) task;
                var publishArgs = ( EbayPublishArgs ) task.Args;

                CreateFeedFile( publishArgs.Feed );

                var mipResponse = MipConnector.SendZippedFeedFolder( publishArgs.Feed );

                EraseFeedFolder( publishArgs.Feed );

                mipResponse.Check();

                ( ( ITask ) publishTask ).Response = new ChannelResponse< EbayPublishResult >(
                    true,
                    ChannelResponseStatusCode.PublishSuccess,
                    new EbayPublishResult( mipResponse.Result.MipRequestId ),
                    mipResponse );

                publishTask.MipRequestStatusCode = MipRequestStatus.Initial;
            }
            catch( Exception exception ) {
                task.Response = new ChannelResponse< EbayPublishResult >(
                    false,
                    ChannelResponseStatusCode.PublishFail,
                    exception );
            }
        }
    }
}