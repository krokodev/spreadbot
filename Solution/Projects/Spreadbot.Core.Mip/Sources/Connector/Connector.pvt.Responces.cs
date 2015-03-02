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
        // NewFailedResponse
        private static Response FailedResponse(StatusCode statusCode, Exception e)
        {
            return new Response(false, statusCode, FailedStatusDescription(statusCode, e));
        }

        // ===================================================================================== []
        // NewSuccessfulResponse
        private static Response SuccessfulResponse(StatusCode statusCode)
        {
            return new Response(true, statusCode)
            {
                StatusDescription = SuccessfulStatusDescription(statusCode)
            };
        }

        // ===================================================================================== []
        // NewSuccessfulResponse
        private static Response SuccessfulResponse(StatusCode statusCode, object result)
        {
            return new Response(true, statusCode)
            {
                StatusDescription = SuccessfulStatusDescription(statusCode, result.ToString()),
                Result = result
            };
        }

        // ===================================================================================== []
        // NewSuccessfulResponse
        private static Response SuccessfulResponse(StatusCode statusCode, object result, Response innerResponse)
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