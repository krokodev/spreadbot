// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayPublishTask.imp.IProceedableTask.cs
// romak_000, 2015-03-20 18:52

using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Tasks;

// !>> Core | EBay | EbayPublishTask.imp.IProceedableTask

namespace Spreadbot.Core.Channels.Ebay.Operations.Tasks
{
    public sealed partial class EbayPublishTask
    {
        // ===================================================================================== []
        // Explicit
        public void AddProceedInfo( ITaskProceedInfo info )
        {
            _taskProceedHelper.AddProceedInfo( info );
        }

        // --------------------------------------------------------[]
        public void AssertCanBeProceeded()
        {
            if( MipRequestStatusCode != MipRequestStatus.Initial &&
                MipRequestStatusCode != MipRequestStatus.Inprocess ) {
                throw new SpreadbotException( "Unexpected Task MipRequestStatusCode: [{0}]", MipRequestStatusCode );
            }
        }

        // ===================================================================================== []
        // Utils
        private readonly TaskProceedHelper _taskProceedHelper = new TaskProceedHelper();
    }
}