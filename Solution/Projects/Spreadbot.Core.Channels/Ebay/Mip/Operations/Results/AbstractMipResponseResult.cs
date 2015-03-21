// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// AbstractMipResponseResult.cs
// romak_000, 2015-03-21 2:11

using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public abstract class AbstractMipResponseResult : IResponseResult
    {
        protected const string Template = "{0}: [{1}]";
        public abstract string Autoinfo { get; }

        public override string ToString()
        {
            return Autoinfo;
        }
    }
}