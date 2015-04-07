// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayPublishTask.imp.IAbstractTask.cs
// Roman, 2015-04-07 12:24 PM

using System.Collections.Generic;
using Nereal.Serialization;
using Spreadbot.Core.Abstracts.Channel.Operations.Responses;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Operations.Results;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Responses;
using Spreadbot.Sdk.Common.Operations.Tasks;
using YamlDotNet.Serialization;

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
                return TaskStatus.Failure;
            }
            switch( MipRequestStatusCode ) {
                case MipRequestStatus.Initial :
                    return TaskStatus.Inprocess;

                case MipRequestStatus.Inprocess :
                    return TaskStatus.Inprocess;

                case MipRequestStatus.Unknown :
                    return TaskStatus.Failure;

                case MipRequestStatus.Failure :
                    return TaskStatus.Failure;

                case MipRequestStatus.Success :
                    return TaskStatus.Success;
            }
            throw new SpreadbotException( "Wrong MipRequestStatusCode [{0}]", MipRequestStatusCode );
        }

        // --------------------------------------------------------[]
        private readonly IEnumerable< IAbstractTask > _abstractSubTasks = new List< IAbstractTask >();

        [YamlMember( Alias = "SubTasks", Order = 100 )]
        public override IEnumerable< IAbstractTask > AbstractSubTasks
        {
            get { return _abstractSubTasks; }
        }

        // --------------------------------------------------------[]
        [YamlMember( Alias = "Response", Order = 90 )]
        public override IAbstractResponse AbstractResponse
        {
            get { return EbayPublishResponse; }
            set { EbayPublishResponse = ( ChannelResponse< EbayPublishResult > ) value; }
        }

        // --------------------------------------------------------[]
        [NotSerialize]
        [YamlIgnore]

        // Is serialized by [AbstractResponse]
        public ChannelResponse< EbayPublishResult > EbayPublishResponse { get; set; }

        // --------------------------------------------------------[]
        public override string GetBriefInfo()
        {
            return string.Format(
                "Channel {3} {0}: {1} {2}",
                GetStatusCode(),
                IsCritical ? "Critical" : "Non critical",
                string.Format( "Publish [{0}]",
                    Args == null ? "n/a" : Args.MipFeedHandler.GetName() ),
                ChannelId );
        }
    }
}