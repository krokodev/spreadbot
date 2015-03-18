namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public abstract class AbstractMipResponseResult: IMipResponseResult
    {
        protected const string Template = "{0}: [{1}]";
        public abstract string Autoinfo { get; }
        public virtual MipRequest.Identifier GetMipRequestId()
        {
            return null;
        }

        public override string ToString()
        {
            return Autoinfo;
        }
    }
}