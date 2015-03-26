// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractTask.state.cs
// romak_000, 2015-03-26 20:14

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
        public bool IsCritical { get; set; }

        [YamlMember( Order = 2 )]
        public string Description { get; set; }

        [YamlMember( Order = 3 )]
        public DateTime CreationTime { get; set; }

        [YamlMember( Order = 4 )]
        public DateTime LastUpdateTime { get; protected set; }
    }
}