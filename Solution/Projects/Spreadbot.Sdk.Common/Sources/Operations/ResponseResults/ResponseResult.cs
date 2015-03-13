namespace Spreadbot.Sdk.Common
{
    public abstract class ResponseResult: IResponseResult
    {
        protected const string Template = "{0}:{1}";
        public abstract string Autoinfo { get; }
        public override string ToString()
        {
            return Autoinfo;
        }
    }
}