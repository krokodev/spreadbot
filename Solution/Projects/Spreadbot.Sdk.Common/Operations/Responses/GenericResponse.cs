// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// GenericResponse.cs
// romak_000, 2015-03-19 15:49

using System;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Sdk.Common.Operations.Responses
{
    public partial class GenericResponse<TR, TC> : IResponse
        where TR : IResponseResult
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
            : this(isSucces, code, result)
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

        public string Autoinfo
        {
            get { return GetAutoinfo(0); }
        }

        public string GetAutoinfo(int level)
        {
            return IsSuccess
                ? GetSuccessAutoinfo(level)
                : GetFailedAutoinfo(level);
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