// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreTask.cs
// romak_000, 2015-03-21 2:11

using System;
using System.Collections.Generic;
using Nereal.Serialization;
using Spreadbot.Core.Abstracts.Chanel.Operations.Tasks;
using Spreadbot.Core.Abstracts.Store.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;
using Spreadbot.Sdk.Common.Operations.Responses;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Stores.Demoshop.Operations.Tasks
{
    public class DemoshopStoreTask : AbstractStoreTask
    {
        private List< AbstractChannelTask > _channelTasks = new List< AbstractChannelTask >();

        [Serialize]
        private List< AbstractChannelTask > ChannelTasks
        {
            get { return _channelTasks; }
            set { _channelTasks = value; }
        }

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
        public override IAbstractResponse AbstractResponse { get; set; }

        public void AddSubTasks( IEnumerable<EbayPublishTask> tasks )
        {
            ChannelTasks.AddRange( tasks );
        }
    }
}