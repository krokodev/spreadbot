namespace Spreadbot.Core.Common
{
    public abstract class ResponseResult: IResponseResult
    {
        protected const string Template = "{0}:{1}";
        public abstract string GetAutoinfo();
        public override string ToString()
        {
            return GetAutoinfo();
        }
    }
}