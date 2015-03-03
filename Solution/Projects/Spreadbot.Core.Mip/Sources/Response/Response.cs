using System;
using Crocodev.Common;

namespace Spreadbot.Core.Mip
{
    public partial class Response<T> : IResponse where T:IResponseResult
    {
        // ===================================================================================== []
        // Public
        public Response(bool isSucces = false, StatusCode code = StatusCode.Unknown)
        {
            IsSuccess = isSucces;
            Code = code;
        }

        public StatusCode Code { get; set; }
        public virtual T Result { get; set; }
        public string Details { get; set; }
        public Exception Exception { get; set; }
        public IResponse InnerResponse { get; set; }


        public virtual string Description
        {
            get { return GetDescription(0); }
        }
        public string GetDescription(int level)
        {
            return IsSuccess
                ? GetSuccessDescription(level)
                : GetFailedDescription(level);
        }

        public void Check()
        {
            if (!IsSuccess)
            {
                throw new ResponseException(this);
            }
        }

        public bool IsSuccess { get; private set; }
    }

    public interface IResponse
    {
        string Description { get; }
        bool IsSuccess { get; }
        string GetDescription(int level);
    }
}