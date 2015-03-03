using System;
using Crocodev.Common;

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        // ===================================================================================== []
        // Failed Status Description
        private static string FailedStatusDescription(StatusCode statusCode, string description)
        {
            return "\nCode=[{0}]\nDescription=[{1}]".SafeFormat(statusCode, description);
        }

        // --------------------------------------------------------[]
        private static string FailedStatusDescription(StatusCode statusCode, Exception e)
        {
            return "\nCode=[{0}]\nDescription=[Exception:{1}]".SafeFormat(statusCode, e.Message);
        }

        // ===================================================================================== []
        // Successful Status Description
        private static string SuccessfulStatusDescription(StatusCode statusCode)
        {
            return "\nCode=[{0}]".SafeFormat(statusCode);
        }

        // --------------------------------------------------------[]
        private static string SuccessfulStatusDescription(StatusCode statusCode, string result)
        {
            return "\nCode=[{0}]\nResult=[{1}]".SafeFormat(statusCode, result);
        }

        // --------------------------------------------------------[]
        private static string SuccessfulStatusDescription(StatusCode statusCode, object result, string description)
        {
            return "\nCode=[{0}]\nResult=[{1}]\nDescription=[{2}]".SafeFormat(statusCode, result, description);
        }
    }
}