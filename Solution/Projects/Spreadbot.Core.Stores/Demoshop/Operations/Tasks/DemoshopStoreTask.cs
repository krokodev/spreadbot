// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreTask.cs
// romak_000, 2015-03-20 20:55

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
        public DemoshopStoreTask()
        {
            ChannelTasks = new List< AbstractChannelTask >();
        }

        [Serialize]
        private List< AbstractChannelTask > ChannelTasks { get; set; }

        [Serialize]
        public override IAbstractResponse AbstractResponse { get; set; }

        public override IEnumerable< IAbstractTask > AbstractSubTasks
        {
            get { return ChannelTasks; }
        }

        public override TaskStatus GetStatusCode()
        {
            return CalcSuperTaskStatusCode();
        }

        public void AddSubTasks( params EbayPublishTask[] tasks )
        {
            ChannelTasks.AddRange( tasks );
        }
    }
}