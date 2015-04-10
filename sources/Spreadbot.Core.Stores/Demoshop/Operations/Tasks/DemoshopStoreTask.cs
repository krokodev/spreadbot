// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreTask.cs
// Roman, 2015-04-10 1:28 PM

using System;
using System.Collections.Generic;
using Nereal.Serialization;
using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;
using Spreadbot.Core.Abstracts.Store.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;
using Spreadbot.Sdk.Common.Operations.Responses;
using Spreadbot.Sdk.Common.Operations.Tasks;
using YamlDotNet.Serialization;

namespace Spreadbot.Core.Stores.Demoshop.Operations.Tasks
{
    public class DemoshopStoreTask : AbstractStoreTask
    {
        private List< AbstractChannelTask > _channelTasks;

        [Serialize]
        private List< AbstractChannelTask > ChannelTasks
        {
            get { return _channelTasks ?? ( _channelTasks = new List< AbstractChannelTask >() ); }
            set { _channelTasks = value; }
        }

        [YamlMember( Alias = "SubTasks", Order = 100 )]
        public override IEnumerable< IAbstractTask > AbstractSubTasks
        {
            get { return ChannelTasks; }
        }

        public override TaskStatus GetStatusCode()
        {
            LastUpdateTime = DateTime.Now;
            return CalcSuperTaskStatusCode();
        }

        [Serialize]
        [YamlMember( Alias = "Response" )]
        public override IAbstractResponse AbstractResponse { get; set; }

        public void AddSubTasks( params EbayPublishTask[] tasks )
        {
            ChannelTasks.AddRange( tasks );
        }
    }
}