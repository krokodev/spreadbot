// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayPublishTask.imp.IProceedableTask.cs
// romak_000, 2015-03-20 13:56

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
        void IProceedableTask.SaveProceedInfo( ITaskProceedInfo info )
        {
            _taskProceedHelper.Save( info );
        }

        // --------------------------------------------------------[]
        void IProceedableTask.AssertCanBeProceeded()
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