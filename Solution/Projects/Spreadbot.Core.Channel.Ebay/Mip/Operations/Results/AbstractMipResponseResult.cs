// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// AbstractMipResponseResult.cs
// romak_000, 2015-03-19 14:02

using System;

namespace Spreadbot.Core.Channel.Ebay.Mip.Operations.Results
{
    public abstract class AbstractMipResponseResult : IMipResponseResult
    {
        protected const string Template = "{0}: [{1}]";
        public abstract string Autoinfo { get; }

        public virtual Guid GetMipRequestId()
        {
            return new Guid();
        }

        public override string ToString()
        {
            return Autoinfo;
        }
    }
}