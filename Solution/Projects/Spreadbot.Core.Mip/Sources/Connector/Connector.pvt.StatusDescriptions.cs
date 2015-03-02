using System;
using Crocodev.Common;

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        // ===================================================================================== []
        // FailedStatusDescription
        private static string FailedStatusDescription(StatusCode statusCode, Exception e)
        {
            return "StatusCode=[{0}] Exception: [{1}]".SafeFormat(statusCode, e.Message);
        }        
        
        // ===================================================================================== []
        // SuccessfulStatusDescription
        private static string SuccessfulStatusDescription(StatusCode statusCode)
        {
            return "StatusCode=[{0}]".SafeFormat(statusCode);
        }

        // ===================================================================================== []
        // SuccessfulStatusDescription
        private static string SuccessfulStatusDescription(StatusCode statusCode, string result)
        {
            return "StatusCode=[{0}] Result=[{1}]".SafeFormat(statusCode, result);
        }

        // ===================================================================================== []
        // SuccessfulStatusDescription
        private static string SuccessfulStatusDescription(StatusCode statusCode,object result,string innerDescription)
        {
            return "StatusCode=[{0}] Result=[{1}] Inner=[{2}]".SafeFormat(statusCode, result, innerDescription);
        }
    }
}