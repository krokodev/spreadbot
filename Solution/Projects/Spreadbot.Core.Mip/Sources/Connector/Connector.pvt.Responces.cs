// ReSharper disable RedundantUsingDirective

using System;
using System.Diagnostics;
using Crocodev.Common;
using Crocodev.Common.Identifier;
using WinSCP;

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        // ===================================================================================== []
        // Failed Responses
        private static Response ResponseFail(StatusCode statusCode, string statusDescription)
        {
            return new Response(false, statusCode, statusDescription);
        }

        private static Response ResponseFail(StatusCode statusCode, Exception e)
        {
            return new Response(false, statusCode, FailedStatusDescription(statusCode, e));
        }

        // ===================================================================================== []
        // Successful Responses
        private static Response ResponseSuccess(StatusCode statusCode)
        {
            return new Response(true, statusCode)
            {
                StatusDescription = SuccessfulStatusDescription(statusCode)
            };
        }
        private static Response ResponseSuccess(StatusCode statusCode, object result)
        {
            return new Response(true, statusCode)
            {
                StatusDescription = SuccessfulStatusDescription(statusCode, result.ToString()),
                Result = result
            };
        }
        private static Response ResponseSuccess(StatusCode statusCode, object result, Response innerResponse)
        {
            Trace.Assert(innerResponse.IsSuccess);
            return new Response(true, statusCode)
            {
                StatusDescription = SuccessfulStatusDescription(statusCode, result, innerResponse.StatusDescription),
                Result = result
            };
        }
    }
}