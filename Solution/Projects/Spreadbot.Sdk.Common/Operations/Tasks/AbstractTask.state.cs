// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractTask.state.cs
// romak_000, 2015-03-20 21:23

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public abstract partial class AbstractTask
    {
        public bool IsCritical { get; set; }
        public string Description { get; set; }
    }
}