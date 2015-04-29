// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// AmazonAdapter.pvt.Tasks.cs

using System;
using Spreadbot.Core.Abstracts.Channel.Operations.Responses;
using Spreadbot.Core.Channels.Amazon.Mws.Connector;
using Spreadbot.Core.Channels.Amazon.Operations.Tasks;

namespace Spreadbot.Core.Channels.Amazon.Adapter
{
    public partial class AmazonAdapter
    {
        private static void DoRunSubmissionTask( AmazonSubmissionTask task )
        {
/*
            try {
                var args = task.Args;

                CreateFeedFile( args.MwsFeedHandler );

                var mipResponse = MwsConnector.Instance.SubmitFeed( args.MwsFeedHandler );

                EraseFeedFolder( args.MwsFeedHandler );

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
*/
        }

        private static void ProceedSubmissionTask( AmazonSubmissionTask task )
        {
            /*            task.AssertCanBeProceeded();

            MwsRequestStatusResponse statusResponse = null;

            try {
                var args = task.Args;
                var mipRequest = new MipRequestHandler(
                    args.MwsFeedHandler,
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
            task.WasUpdatedNow();*/
        }

        /*        private void DoProceedTask( EbaySubmissionTask task )
        {
            switch( task.ChannelMethod ) {
                case ChannelMethod.Submit :
                    ProceedSubmissionTask( task );
                    break;
                default :
                    throw new SpreadbotException( "Unexpected Task Channel Method: [{0}]", task.ChannelMethod );
            }
        }*/
    }
}