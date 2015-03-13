using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.Common
{
    public abstract class MipResponseResult: IResponseResult
    {
        protected const string Template = "{0}: [{1}]";
        public abstract string Autoinfo { get; }
        public override string ToString()
        {
            return Autoinfo;
        }
    }
}