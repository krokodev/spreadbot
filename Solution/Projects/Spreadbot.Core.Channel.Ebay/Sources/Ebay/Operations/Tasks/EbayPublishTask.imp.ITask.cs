using Crocodev.Common;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Sdk.Common;

// !>> Core | EBay | EbayPublishTask.imp.ITask

namespace Spreadbot.Core.Channel.Ebay
{
    public sealed partial class EbayPublishTask
    {
        // ===================================================================================== []
        // GetStatusCode
        public override TaskStatus GetStatusCode()
        {
            if (((IHierarchicalTask) this).Response == null)
            {
                return TaskStatus.Todo;
            }
            if (!((IHierarchicalTask) this).Response.IsSuccess)
            {
                return TaskStatus.Fail;
            }
            switch (MipRequestStatusCode)
            {
                case MipRequestStatus.Initial:
                    return TaskStatus.Inprocess;

                case MipRequestStatus.Inprocess:
                    return TaskStatus.Inprocess;

                case MipRequestStatus.Unknown:
                    return TaskStatus.Fail;

                case MipRequestStatus.Fail:
                    return TaskStatus.Fail;

                case MipRequestStatus.Success:
                    return TaskStatus.Success;
            }
            throw new SpreadbotException("Wrong MipRequestStatusCode [{0}]", MipRequestStatusCode);
        }

        // ===================================================================================== []
        // Autoinfo
        public override string Autoinfo
        {
            get
            {
                return string.Format(
                    "Channel {3} {0}: {1} {2}",
                    GetStatusCode(),
                    IsCritical ? "Critical" : "Non critical",
                    "Publish [{0}]".TryFormat(((EbayPublishArgs)AbstractArgs).Feed.Name),
                    ChannelRef.Name
                    );
            }
        }
    }
}