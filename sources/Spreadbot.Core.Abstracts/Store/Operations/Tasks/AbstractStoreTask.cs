// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// AbstractStoreTask.cs
// Roman, 2015-04-01 9:09 PM

using Spreadbot.Sdk.Common.Operations.Tasks;
using YamlDotNet.Serialization;

namespace Spreadbot.Core.Abstracts.Store.Operations.Tasks
{
    public abstract class AbstractStoreTask : AbstractTask, IStoreTask
    {
        public override string GetBriefInfo()
        {
            return string.Format(
                "Store {2} {0}: {1}",
                GetStatusCode(),
                Description,
                StoreId
                );
        }

        [YamlMember( Order = 19 )]
        public string StoreId { get; set; }
    }
}