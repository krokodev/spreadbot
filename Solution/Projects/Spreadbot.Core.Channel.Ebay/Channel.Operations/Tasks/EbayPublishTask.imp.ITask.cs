// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// EbayPublishTask.imp.ITask.cs
// romak_000, 2015-03-19 14:02

using Spreadbot.Core.Channel.Ebay.Channel.Operations.Args;
using Spreadbot.Core.Channel.Ebay.Mip.Operations.Request;
using Spreadbot.Sdk.Common.Crocodev.Common.Etensions;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Tasks;

// !>> Core | EBay | EbayPublishTask.imp.ITask

namespace Spreadbot.Core.Channel.Ebay.Channel.Operations.Tasks
{
    public sealed partial class EbayPublishTask
    {
        // ===================================================================================== []
        // GetStatusCode
        public override TaskStatus GetStatusCode()
        {
            if (((ITask) this).Response == null)
            {
                return TaskStatus.Todo;
            }
            if (!((ITask) this).Response.IsSuccess)
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
                    "Publish [{0}]".TryFormat(((EbayPublishArgs) AbstractArgs).Feed.Name),
                    ChannelRef.Name
                    );
            }
        }
    }
}