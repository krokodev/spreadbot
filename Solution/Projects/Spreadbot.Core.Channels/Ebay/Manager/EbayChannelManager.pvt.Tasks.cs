// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayChannelManager.pvt.Tasks.cs
// romak_000, 2015-03-20 20:01

using System;
using Spreadbot.Core.Abstracts.Chanel.Operations.Methods;
using Spreadbot.Core.Abstracts.Chanel.Operations.Responses;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Core.Channels.Ebay.Manager
{
    public partial class EbayChannelManager
    {
        // --------------------------------------------------------[]
        private static void ProceedPublishTask( EbayPublishTask ebayPublishTask )
        {
            ebayPublishTask.AssertCanBeProceeded();

            MipRequestStatusResponse statusResponse = null;

            try {
                var ebayPublishArgs = ebayPublishTask.Args;
                var mipRequest = new MipRequest(
                    ebayPublishArgs.MipFeedHandler,
                    ebayPublishTask.EbayPublishResponse.Result.MipRequestId );

                statusResponse = MipConnector.GetRequestStatus( mipRequest );
                statusResponse.Check();

                ebayPublishTask.MipRequestStatusCode = statusResponse.Result.MipRequestStatusCode;
                ebayPublishTask.AddProceedInfo( statusResponse );
            }
            catch {
                ebayPublishTask.MipRequestStatusCode = MipRequestStatus.Fail;
                ebayPublishTask.AddProceedInfo( statusResponse );
            }
        }

        // --------------------------------------------------------[]
        private void DoProceedTask( EbayPublishTask task )
        {
            switch( task.ChannelMethod ) {
                case ChannelMethod.Publish :
                    ProceedPublishTask( task );
                    break;
                default :
                    throw new SpreadbotException( "Unexpected Task Channel Method: [{0}]", task.ChannelMethod );
            }
        }

        // --------------------------------------------------------[]
        private static void DoRunPublishTask( EbayPublishTask publishTask )
        {
            try {
                var publishArgs = publishTask.Args;

                CreateFeedFile( publishArgs.MipFeedHandler );

                var mipResponse = MipConnector.SendZippedFeedFolder( publishArgs.MipFeedHandler );

                EraseFeedFolder( publishArgs.MipFeedHandler );

                mipResponse.Check();

                publishTask.EbayPublishResponse = new ChannelResponse< EbayPublishResult >(
                    true,
                    ChannelResponseStatusCode.PublishSuccess,
                    new EbayPublishResult( mipResponse.Result.MipRequestId ),
                    mipResponse );

                publishTask.MipRequestStatusCode = MipRequestStatus.Initial;
            }
            catch( Exception exception ) {
                publishTask.EbayPublishResponse = new ChannelResponse< EbayPublishResult >(
                    false,
                    ChannelResponseStatusCode.PublishFail,
                    exception );
            }
        }
    }
}