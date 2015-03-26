// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayChannelManager.pvt.Tasks.cs
// romak_000, 2015-03-26 19:42

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
        private static void ProceedPublishTask( EbayPublishTask task )
        {
            task.AssertCanBeProceeded();

            MipRequestStatusResponse statusResponse = null;

            try {
                var ebayPublishArgs = task.Args;
                var mipRequest = new MipRequestHandler(
                    ebayPublishArgs.MipFeedHandler,
                    task.EbayPublishResponse.Result.MipRequestId );

                statusResponse = MipConnector.GetRequestStatus( mipRequest );
                statusResponse.Check();

                task.MipRequestStatusCode = statusResponse.Result.MipRequestStatusCode;
                task.EbayPublishResponse.Result.MipItemId = statusResponse.Result.MipItemId;

                // Code: task.AddProceedInfo( statusResponse );
                statusResponse.ProceedTime = DateTime.Now;
                task.AddProceedInfo( statusResponse );
            }
            catch {
                task.MipRequestStatusCode = MipRequestStatus.Failure;
                task.AddProceedInfo( statusResponse );
            }
            task.WasUpdatedNow();
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
        private static void DoRunPublishTask( EbayPublishTask task )
        {
            try {
                var publishArgs = task.Args;

                CreateFeedFile( publishArgs.MipFeedHandler );

                var mipResponse = MipConnector.SendZippedFeedFolder( publishArgs.MipFeedHandler );

                EraseFeedFolder( publishArgs.MipFeedHandler );

                mipResponse.Check();

                task.EbayPublishResponse = new ChannelResponse< EbayPublishResult >(
                    true,
                    ChannelResponseStatusCode.PublishSuccess,
                    new EbayPublishResult { MipRequestId = mipResponse.Result.MipRequestId },
                    mipResponse );
                task.MipRequestStatusCode = MipRequestStatus.Initial;
            }
            catch( Exception exception ) {
                task.EbayPublishResponse = new ChannelResponse< EbayPublishResult >(
                    false,
                    ChannelResponseStatusCode.PublishFailure,
                    exception );
            }
            task.WasUpdatedNow();
        }
    }
}