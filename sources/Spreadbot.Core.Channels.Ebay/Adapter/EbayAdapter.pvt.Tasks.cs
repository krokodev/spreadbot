// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// EbayAdapter.pvt.Tasks.cs

using System;
using Spreadbot.Core.Abstracts.Channel.Operations.Methods;
using Spreadbot.Core.Abstracts.Channel.Operations.Responses;
using Spreadbot.Core.Channels.Ebay.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.FeedSubmission;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Proceed;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Ebay.Adapter
{
    public partial class EbayAdapter
    {
        // --------------------------------------------------------[]
        private static void ProceedSubmissionTask( EbaySubmissionTask task )
        {
            task.AssertCanBeProceeded();

            Response< MipGetSubmissionStatusResult > statusResponse = null;

            try {
                var args = task.Args;
                var submission = new MipFeedSubmissionDescriptor(
                    args.MwsFeedDescriptor,
                    task.EbaySubmissionResponse.Result.MipSubmissionId );

                statusResponse = MipConnector.Instance.GetSubmissionStatus( submission );
                statusResponse.Check();

                task.MipFeedSubmissionResultStatusCode = statusResponse.Result.MipFeedSubmissionResultStatusCode;
                task.EbaySubmissionResponse.Result.MipItemId = statusResponse.Result.MipItemId;

                task.AddProceedInfo( GetProceedInfo( statusResponse ) );
            }
            catch {
                task.MipFeedSubmissionResultStatusCode = MipFeedSubmissionResultStatus.Failure;
                task.AddProceedInfo( GetProceedInfo( statusResponse ) );
            }
            task.WasUpdatedNow();
        }

        // --------------------------------------------------------[]
        private static TaskProceedInfo< Response< MipGetSubmissionStatusResult > > GetProceedInfo(
            Response< MipGetSubmissionStatusResult > statusResponse )
        {
            return new TaskProceedInfo< Response< MipGetSubmissionStatusResult > >( statusResponse );
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
                    Result = new EbaySubmissionResult { MipSubmissionId = mipResponse.Result.FeedSubmissionId },
                    InnerResponses = { mipResponse }
                };
                task.MipFeedSubmissionResultStatusCode = MipFeedSubmissionResultStatus.Initial;
            }
            catch( Exception exception ) {
                task.EbaySubmissionResponse = new ChannelResponse< EbaySubmissionResult >( exception );
            }
            task.WasUpdatedNow();
        }
    }
}