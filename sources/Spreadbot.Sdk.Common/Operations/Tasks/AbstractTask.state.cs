// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractTask.state.cs
// Roman, 2015-04-01 4:59 PM

using System;
using YamlDotNet.Serialization;

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public abstract partial class AbstractTask
    {
        [YamlMember( Order = -1 )]
        public string Type
        {
            get { return GetType().ToString(); }

            // ReSharper disable once ValueParameterNotUsed
            set { }
        }

        [YamlMember( Alias = "TaskId", Order = 0 )]
        public string Id { get; private set; }

        [YamlMember( Order = 1 )]
        public string BriefInfo
        {
            get { return GetBriefInfo(); }

            // ReSharper disable once ValueParameterNotUsed
            set { }
        }

        [YamlMember( Order = 2 )]
        public TaskStatus StatusCode
        {
            get { return GetStatusCode(); }

            // ReSharper disable once ValueParameterNotUsed
            set { }
        }

        [YamlMember( Order = 4 )]
        public bool IsCritical { get; set; }

        [YamlMember( Order = 5 )]
        public string Description { get; set; }

        [YamlMember( Order = 6 )]
        public DateTime CreationTime { get; set; }

        [YamlMember( Order = 7 )]
        public DateTime LastUpdateTime { get; protected set; }
    }
}