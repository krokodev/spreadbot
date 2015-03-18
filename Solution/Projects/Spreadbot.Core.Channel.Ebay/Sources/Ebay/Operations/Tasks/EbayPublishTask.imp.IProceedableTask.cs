using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Sdk.Common;

// !>> Core | EBay | EbayPublishTask.imp.IProceedableTask

namespace Spreadbot.Core.Channel.Ebay
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