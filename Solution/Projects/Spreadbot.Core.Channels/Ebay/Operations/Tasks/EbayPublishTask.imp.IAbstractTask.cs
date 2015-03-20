// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayPublishTask.imp.IAbstractTask.cs
// romak_000, 2015-03-21 0:28

using System.Collections.Generic;
using Crocodev.Common.Extensions;
using Nereal.Serialization;
using Spreadbot.Core.Abstracts.Chanel.Operations.Responses;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Operations.Results;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Responses;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Channels.Ebay.Operations.Tasks
{
    public sealed partial class EbayPublishTask
    {
        // --------------------------------------------------------[]
        public override TaskStatus GetStatusCode()
        {
            if( AbstractResponse == null ) {
                return TaskStatus.Todo;
            }
            if( !AbstractResponse.IsSuccess ) {
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

        // --------------------------------------------------------[]
        private readonly IEnumerable< IAbstractTask > _abstractSubTasks = new List< IAbstractTask >();
        public override IEnumerable< IAbstractTask > AbstractSubTasks
        {
            get { return _abstractSubTasks; }
        }

        // --------------------------------------------------------[]
        public override IAbstractResponse AbstractResponse
        {
            get { return EbayPublishResponse; }
            set { EbayPublishResponse = ( ChannelResponse< EbayPublishResult > ) value; }
        }

        // --------------------------------------------------------[]
        public override string GetAutoinfo()
        {
            return string.Format(
                "Channel {3} {0}: {1} {2}",
                GetStatusCode(),
                IsCritical ? "Critical" : "Non critical",
                "Publish [{0}]".TryFormat( Args.MipFeedHandler.GetName() ),
                ChannelId );
        }
    }
}