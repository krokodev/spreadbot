// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractResponseResult.cs
// romak_000, 2015-03-19 13:44

namespace Spreadbot.Sdk.Common.Operations.ResponseResults
{
    public abstract class AbstractResponseResult : IResponseResult
    {
        protected const string Template = "{0}:{1}";
        public abstract string Autoinfo { get; }

        public override string ToString()
        {
            return Autoinfo;
        }
    }
}