// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// EbayPublishTask.imp.IProceedableTask.cs
// romak_000, 2015-03-19 15:37

using Spreadbot.Core.Channel.Ebay.Mip.Operations.Request;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Tasks;

// !>> Core | EBay | EbayPublishTask.imp.IProceedableTask

namespace Spreadbot.Core.Channel.Ebay.Channel.Operations.Tasks
{
    public sealed partial class EbayPublishTask
    {
        // ===================================================================================== []
        // Explicit
        void IProceedableTask.SaveProceedInfo(ITaskProceedInfo info)
        {
            _taskProceedHelper.Save(info);
        }

        // --------------------------------------------------------[]
        void IProceedableTask.AssertCanBeProceeded()
        {
            if (MipRequestStatusCode != MipRequestStatus.Initial &&
                MipRequestStatusCode != MipRequestStatus.Inprocess)
            {
                throw new SpreadbotException("Unexpected Task MipRequestStatusCode: [{0}]", MipRequestStatusCode);
            }
        }

        // ===================================================================================== []
        // Utils
        private readonly TaskProceedHelper _taskProceedHelper = new TaskProceedHelper();
    }
}