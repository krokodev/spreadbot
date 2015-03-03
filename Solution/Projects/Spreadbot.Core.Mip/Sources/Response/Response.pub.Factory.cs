using System;
using System.Diagnostics;

namespace Spreadbot.Core.Mip
{
    public partial class Response<T>
    {
        // ===================================================================================== []
        // Failed Responses
        public static Response<T> NewFail(StatusCode statusCode, string details)
        {
            return new Response<T>(false, statusCode)
            {
                Details = details
            };
        }

        // --------------------------------------------------------[]
        public static Response<T> NewFail(StatusCode statusCode, Exception e)
        {
            return new Response<T>(false, statusCode)
            {
                Exception =  e
            };
        }

        // ===================================================================================== []
        // Successful Responses
        public static Response<T> NewSuccess(StatusCode statusCode)
        {
            return new Response<T>(true, statusCode);
        }
        // --------------------------------------------------------[]
        public static Response<T> NewSuccess(StatusCode statusCode, T result)
        {
            return new Response<T>(true, statusCode)
            {
                Result = result
            };
        }
        // --------------------------------------------------------[]
        public static Response<T> NewSuccess(StatusCode statusCode, T result, IResponse innerResponse)
        {
            Trace.Assert(innerResponse.IsSuccess);
            return new Response<T>(true, statusCode)
            {
                Result = result,
                InnerResponse = innerResponse
            };
        }
        // --------------------------------------------------------[]
        public static Response<T> NewSuccess(StatusCode statusCode, T result, string details)
        {
            return new Response<T>(true, statusCode)
            {
                Details = details,
                Result = result
            };
        }
    }
}