// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// EbaySubmissionTask.imp.IProceedableTask.cs

using System.Collections.Generic;
using Spreadbot.Core.Channels.Ebay.Services.Mip.FeedSubmission;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Proceed;
using YamlDotNet.Serialization;

namespace Spreadbot.Core.Channels.Ebay.Operations.Tasks
{
    public sealed partial class EbaySubmissionTask
    {
        public void AddProceedInfo( ITaskProceedInfo info )
        {
            ProceedHistory.Add( info );
        }

        public void AssertCanBeProceeded()
        {
            if( MipFeedSubmissionResultStatusCode != MipFeedSubmissionResultStatus.Initial &&
                MipFeedSubmissionResultStatusCode != MipFeedSubmissionResultStatus.Inprocess ) {
                throw new SpreadbotException( "Unexpected Task MipSubmissionStatusCode: [{0}]",
                    MipFeedSubmissionResultStatusCode );
            }
        }

        [YamlMember( Order = 99 )]
        public List< ITaskProceedInfo > ProceedHistory { get; set; }

        public IEnumerable< ITaskProceedInfo > GetProceedHistory()
        {
            return ProceedHistory;
        }
    }
}