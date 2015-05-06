// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// EbaySubmissionTask.imp.IAbstractTask.cs

using System.Collections.Generic;
using Nereal.Serialization;
using Spreadbot.Core.Abstracts.Channel.Operations.Responses;
using Spreadbot.Core.Channels.Ebay.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Services.Mip.FeedSubmission;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Responses;
using Spreadbot.Sdk.Common.Operations.Tasks;
using YamlDotNet.Serialization;

namespace Spreadbot.Core.Channels.Ebay.Operations.Tasks
{
    public sealed partial class EbaySubmissionTask
    {
        // --------------------------------------------------------[]
        public override TaskStatus GetStatusCode()
        {
            if( AbstractResponse == null ) {
                return TaskStatus.Todo;
            }
            if( !AbstractResponse.IsSuccessful ) {
                return TaskStatus.Failure;
            }
            switch( MipFeedSubmissionOverallStatus ) {
                case MipFeedSubmissionOverallStatus.InProgress :
                    return TaskStatus.InProgress;

                case MipFeedSubmissionOverallStatus.Unknown :
                    return TaskStatus.Failure;

                case MipFeedSubmissionOverallStatus.Failure :
                    return TaskStatus.Failure;

                case MipFeedSubmissionOverallStatus.Success :
                    return TaskStatus.Success;
            }
            throw new SpreadbotException( "Wrong MipSubmissionStatusCode [{0}]", MipFeedSubmissionOverallStatus );
        }

        // --------------------------------------------------------[]
        private IEnumerable< IAbstractTask > _abstractSubTasks;

        [YamlMember( Alias = "SubTasks", Order = 100 )]
        public override IEnumerable< IAbstractTask > AbstractSubTasks
        {
            get { return _abstractSubTasks ?? ( _abstractSubTasks = new List< IAbstractTask >() ); }
        }

        // --------------------------------------------------------[]
        [YamlMember( Alias = "Response", Order = 90 )]
        public override IAbstractResponse AbstractResponse
        {
            get { return EbaySubmissionResponse; }
            set { EbaySubmissionResponse = ( ChannelResponse< EbaySubmissionResult > ) value; }
        }

        // --------------------------------------------------------[]
        [NotSerialize]
        [YamlIgnore]

        // Is serialized by [AbstractResponse]
        public ChannelResponse< EbaySubmissionResult > EbaySubmissionResponse { get; set; }

        // --------------------------------------------------------[]
        public override string GetBriefInfo()
        {
            return string.Format(
                "Channel {3} {0}: {1} {2}",
                GetStatusCode(),
                IsCritical ? "Critical" : "Non critical",
                string.Format( "Submit [{0}]",
                    Args == null ? "n/a" : Args.MipFeedDescriptor.GetName() ),
                ChannelId );
        }
    }
}