// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// AmazonSubmissionTask.imp.IProceedableTask.cs

using System.Collections.Generic;
using Spreadbot.Core.Channels.Amazon.Services.Mws.FeedSubmission;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Proceed;
using YamlDotNet.Serialization;

namespace Spreadbot.Core.Channels.Amazon.Operations.Tasks
{
    public sealed partial class AmazonSubmissionTask
    {
        public void AddProceedInfo( ITaskProceedInfo info )
        {
            ProceedHistory.Add( info );
        }

        public void AssertCanBeProceeded()
        {
            if( MwsFeedSubmissionOverallStatus != MwsFeedSubmissionOverallStatus.InProgress ) {
                throw new SpreadbotException( "Unexpected Task MwsRequestStatusCode: [{0}]",
                    MwsFeedSubmissionOverallStatus );
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