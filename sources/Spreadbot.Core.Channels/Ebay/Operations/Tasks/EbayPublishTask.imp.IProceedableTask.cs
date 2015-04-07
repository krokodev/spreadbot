// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayPublishTask.imp.IProceedableTask.cs
// Roman, 2015-04-07 2:58 PM

using System.Collections.Generic;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Tasks;
using YamlDotNet.Serialization;

namespace Spreadbot.Core.Channels.Ebay.Operations.Tasks
{
    public sealed partial class EbayPublishTask
    {
        public void AddProceedInfo( ITaskProceedInfo info )
        {
            ProceedHistory.Add( info );
        }

        public void AssertCanBeProceeded()
        {
            if( MipRequestStatusCode != MipRequestStatus.Initial &&
                MipRequestStatusCode != MipRequestStatus.Inprocess ) {
                throw new SpreadbotException( "Unexpected Task MipRequestStatusCode: [{0}]", MipRequestStatusCode );
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