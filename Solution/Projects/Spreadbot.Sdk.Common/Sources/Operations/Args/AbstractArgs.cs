namespace Spreadbot.Sdk.Common
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