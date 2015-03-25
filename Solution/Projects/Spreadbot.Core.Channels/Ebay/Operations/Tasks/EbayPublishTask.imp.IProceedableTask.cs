// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayPublishTask.imp.IProceedableTask.cs
// romak_000, 2015-03-25 21:51

using System.Collections.Generic;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Tasks;

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

        public readonly List< ITaskProceedInfo > ProceedHistory = new List< ITaskProceedInfo >();

        public IEnumerable< ITaskProceedInfo > GetProceedHistory()
        {
            return ProceedHistory;
        }
    }
}