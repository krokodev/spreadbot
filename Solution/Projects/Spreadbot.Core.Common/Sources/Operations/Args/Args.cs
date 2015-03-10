namespace Spreadbot.Core.Common
{
    public abstract class Args : IArgs
    {
        public override string ToString()
        {
            return Autoinfo;
        }
        public abstract string Autoinfo { get; }
    }
}