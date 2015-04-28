﻿// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// AmazonSubmissionTask.imp.IAbstractTask.cs

using System.Collections.Generic;
using Nereal.Serialization;
using Spreadbot.Core.Abstracts.Channel.Operations.Responses;
using Spreadbot.Core.Channels.Amazon.Mws.Operations.Request;
using Spreadbot.Core.Channels.Amazon.Operations.Results;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Responses;
using Spreadbot.Sdk.Common.Operations.Tasks;
using YamlDotNet.Serialization;

namespace Spreadbot.Core.Channels.Amazon.Operations.Tasks
{
    public sealed partial class AmazonSubmissionTask
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
            switch( MwsRequestStatusCode ) {
                case MwsRequestStatus.Initial :
                    return TaskStatus.Inprocess;

                case MwsRequestStatus.Inprocess :
                    return TaskStatus.Inprocess;

                case MwsRequestStatus.Unknown :
                    return TaskStatus.Failure;

                case MwsRequestStatus.Failure :
                    return TaskStatus.Failure;

                case MwsRequestStatus.Success :
                    return TaskStatus.Success;
            }
            throw new SpreadbotException( "Wrong MwsRequestStatusCode [{0}]", MwsRequestStatusCode );
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
            get { return AmazonSubmissionResponse; }
            set { AmazonSubmissionResponse = ( ChannelResponse< AmazonSubmissionResult > ) value; }
        }

        // --------------------------------------------------------[]
        [NotSerialize]
        [YamlIgnore]

        // Is serialized by [AbstractResponse]
        public ChannelResponse< AmazonSubmissionResult > AmazonSubmissionResponse { get; set; }

        // --------------------------------------------------------[]
        public override string GetBriefInfo()
        {
            return string.Format(
                "Channel {3} {0}: {1} {2}",
                GetStatusCode(),
                IsCritical ? "Critical" : "Non critical",
                string.Format( "Submit [{0}]",
                    Args == null ? "n/a" : Args.MwsFeedHandler.GetName() ),
                ChannelId );
        }
    }
}