// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// EbayAdapter.pvt.Tasks.cs

using System;
using Spreadbot.Core.Abstracts.Channel.Operations.Methods;
using Spreadbot.Core.Abstracts.Channel.Operations.Responses;
using Spreadbot.Core.Channels.Ebay.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Responses;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Submission;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Core.Channels.Ebay.Adapter
{
    public partial class EbayAdapter
    {
        // --------------------------------------------------------[]
        private static void ProceedSubmissionTask( EbaySubmissionTask task )
        {
            task.AssertCanBeProceeded();

            MipSubmissionStatusResponse statusResponse = null;

            try {
                var args = task.Args;
                var submission = new MipSubmissionDescriptor(
                    args.MwsFeedDescriptor,
                    task.EbaySubmissionResponse.Result.MipSubmissionId );

                statusResponse = MipConnector.Instance.GetSubmissionStatus( submission );
                statusResponse.Check();

                task.MipSubmissionStatusCode = statusResponse.Result.MipSubmissionStatusCode;
                task.EbaySubmissionResponse.Result.MipItemId = statusResponse.Result.MipItemId;

                statusResponse.ProceedTime = DateTime.Now;
                task.AddProceedInfo( statusResponse );
            }
            catch {
                task.MipSubmissionStatusCode = MipSubmissionStatus.Failure;
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

                CreateFeedFile( args.MwsFeedDescriptor );

                var mipResponse = MipConnector.Instance.SubmitFeed( args.MwsFeedDescriptor );

                EraseFeedFolder( args.MwsFeedDescriptor );

                mipResponse.Check();

                task.EbaySubmissionResponse = new ChannelResponse< EbaySubmissionResult > {
                    StatusCode = ChannelResponseStatusCode.SubmitSuccess,
                    Result = new EbaySubmissionResult { MipSubmissionId = mipResponse.Result.FeedSubmissionId },
                    InnerResponses = { mipResponse }
                };
                task.MipSubmissionStatusCode = MipSubmissionStatus.Initial;
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