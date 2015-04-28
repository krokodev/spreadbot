// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// EbayChannelManager.pvt.Tasks.cs

using System;
using Spreadbot.Core.Abstracts.Channel.Operations.Methods;
using Spreadbot.Core.Abstracts.Channel.Operations.Responses;
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
        private static void ProceedSubmissionTask( EbaySubmissionTask task )
        {
            task.AssertCanBeProceeded();

            MipRequestStatusResponse statusResponse = null;

            try {
                var args = task.Args;
                var mipRequest = new MipRequestHandler(
                    args.MipFeedHandler,
                    task.EbaySubmissionResponse.Result.MipRequestId );

                statusResponse = MipConnector.Instance.GetRequestStatus( mipRequest );
                statusResponse.Check();

                task.MipRequestStatusCode = statusResponse.Result.MipRequestStatusCode;
                task.EbaySubmissionResponse.Result.MipItemId = statusResponse.Result.MipItemId;

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
        private void DoProceedTask( EbaySubmissionTask task )
        {
            switch( task.ChannelMethod ) {
                case ChannelMethod.Submit :
                    ProceedSubmissionTask( task );
                    break;
                default :
                    throw new SpreadbotException( "Unexpected Task Channel Method: [{0}]", task.ChannelMethod );
            }
        }

        // --------------------------------------------------------[]
        private static void DoRunSubmissionTask( EbaySubmissionTask task )
        {
            try {
                var args = task.Args;

                CreateFeedFile( args.MipFeedHandler );

                var mipResponse = MipConnector.Instance.SendFeed( args.MipFeedHandler );

                EraseFeedFolder( args.MipFeedHandler );

                mipResponse.Check();

                task.EbaySubmissionResponse = new ChannelResponse< EbaySubmissionResult > {
                    StatusCode = ChannelResponseStatusCode.SubmitSuccess,
                    Result = new EbaySubmissionResult { MipRequestId = mipResponse.Result.MipRequestId },
                    InnerResponses = { mipResponse }
                };
                task.MipRequestStatusCode = MipRequestStatus.Initial;
            }
            catch( Exception exception ) {
                task.EbaySubmissionResponse = new ChannelResponse< EbaySubmissionResult >( exception ) {
                    StatusCode = ChannelResponseStatusCode.SubmitFailure
                };
            }
            task.WasUpdatedNow();
        }
    }
}