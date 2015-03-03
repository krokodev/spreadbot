using System;
using System.Diagnostics;

namespace Spreadbot.Core.Mip
{
    public partial class Response
    {
        // ===================================================================================== []
        // Failed Responses
        public static Response NewFail(StatusCode statusCode, string statusDescription)
        {
            return new Response(false, statusCode, FailedStatusDescription(statusCode, statusDescription));
        }

        // --------------------------------------------------------[]
        public static Response NewFail(StatusCode statusCode, Exception e)
        {
            return new Response(false, statusCode, FailedStatusDescription(statusCode, e));
        }

        // ===================================================================================== []
        // Successful Responses
        public static Response NewSuccess(StatusCode statusCode)
        {
            return new Response(true, statusCode)
            {
                StatusDescription = SuccessfulStatusDescription(statusCode)
            };
        }
        // --------------------------------------------------------[]
        public static Response NewSuccess(StatusCode statusCode, object result)
        {
            return new Response(true, statusCode)
            {
                StatusDescription = SuccessfulStatusDescription(statusCode, result.ToString()),
                Result = result
            };
        }
        // --------------------------------------------------------[]
        public static Response NewSuccess(StatusCode statusCode, object result, Response innerResponse)
        {
            Trace.Assert(innerResponse.IsSuccess);
            return new Response(true, statusCode)
            {
                StatusDescription = SuccessfulStatusDescription(statusCode, result, innerResponse.StatusDescription),
                Result = result
            };
        }
        // --------------------------------------------------------[]
        public static Response NewSuccess(StatusCode statusCode, object result, string description)
        {
            return new Response(true, statusCode)
            {
                StatusDescription = SuccessfulStatusDescription(statusCode, result, description),
                Result = result
            };
        }
    }
}