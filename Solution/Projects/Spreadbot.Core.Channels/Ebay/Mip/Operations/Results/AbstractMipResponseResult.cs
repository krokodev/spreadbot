// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// AbstractMipResponseResult.cs
// romak_000, 2015-03-20 13:56

using System;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
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