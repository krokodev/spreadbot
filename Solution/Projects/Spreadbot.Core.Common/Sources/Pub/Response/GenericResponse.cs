using System;

namespace Spreadbot.Core.Common
{
    public partial class GenericResponse<TR,TC> : IResponse where TR:IResponseResult
    {
        // ===================================================================================== []
        // Protected
        protected GenericResponse(bool isSucces, TC code)
        {
            IsSuccess = isSucces;
            Code = code;
        }

        protected GenericResponse(bool isSucces, TC code, Exception exception)
            : this(isSucces, code)

        {
            Exception = exception;
        }

        protected GenericResponse(bool isSucces, TC code, TR result)
            : this(isSucces, code)
        {
            Result = result;
        }

        protected GenericResponse(bool isSucces, TC code, TR result, IResponse innerResponse)
            :this(isSucces,code,result)
        {
            InnerResponse = innerResponse;
        }

        protected GenericResponse(bool isSucces, TC code, string details)
            : this(isSucces, code)
        {
            Details = details;
        }

        // ===================================================================================== []
        // Public
        public TC Code { get; set; }
        public TR Result { get; set; }
        public string Details { get; set; }
        public Exception Exception { get; set; }
        public IResponse InnerResponse { get; set; }


        public string Description
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
}