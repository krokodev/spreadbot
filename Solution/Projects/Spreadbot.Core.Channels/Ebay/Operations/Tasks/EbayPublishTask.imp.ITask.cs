// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayPublishTask.imp.ITask.cs
// romak_000, 2015-03-20 14:24

using Crocodev.Common.Extensions;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Operations.Args;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Tasks;

// !>> Core | EBay | EbayPublishTask.imp.ITask

namespace Spreadbot.Core.Channels.Ebay.Operations.Tasks
{
    public sealed partial class EbayPublishTask
    {
        // ===================================================================================== []
        // GetStatusCode
        public override TaskStatus GetStatusCode()
        {
            if( ( ( ITask ) this ).Response == null ) {
                return TaskStatus.Todo;
            }
            if( !( ( ITask ) this ).Response.IsSuccess ) {
                return TaskStatus.Fail;
            }
            switch( MipRequestStatusCode ) {
                case MipRequestStatus.Initial :
                    return TaskStatus.Inprocess;

                case MipRequestStatus.Inprocess :
                    return TaskStatus.Inprocess;

                case MipRequestStatus.Unknown :
                    return TaskStatus.Fail;

                case MipRequestStatus.Fail :
                    return TaskStatus.Fail;

                case MipRequestStatus.Success :
                    return TaskStatus.Success;
            }
            throw new SpreadbotException( "Wrong MipRequestStatusCode [{0}]", MipRequestStatusCode );
        }

        // ===================================================================================== []
        // Autoinfo
        public override string GetAutoinfo()
        {
            return string.Format(
                "Channel {3} {0}: {1} {2}",
                GetStatusCode(),
                IsCritical ? "Critical" : "Non critical",
                "Publish [{0}]".TryFormat( ( ( EbayPublishArgs ) AbstractArgs ).FeedHandler.Name ),
                ChannelId );
        }
    }
}