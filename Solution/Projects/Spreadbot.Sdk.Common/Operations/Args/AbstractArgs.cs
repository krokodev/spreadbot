// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractArgs.cs
// romak_000, 2015-03-19 15:38

namespace Spreadbot.Sdk.Common.Operations.Args
{
    public abstract class AbstractArgs : ITaskArgs
    {
        public override string ToString()
        {
            return Autoinfo;
        }

        public abstract string Autoinfo { get; }
    }
}