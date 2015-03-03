using System;
using Crocodev.Common;

namespace Spreadbot.Core.Mip
{
    public partial class Response
    {
        // ===================================================================================== []
        // Failed Status Description
        private static string FailedStatusDescription(StatusCode code)
        {
            return DescriptionElement("Code", code);
        }

        // --------------------------------------------------------[]
        private static string FailedStatusDescription(StatusCode code, string details)
        {
            return SuccessfulStatusDescription(code)
                   + DescriptionElement("Details", details);
        }

        // --------------------------------------------------------[]
        private static string FailedStatusDescription(StatusCode code, Exception e)
        {
            return SuccessfulStatusDescription(code)
                + ExceptionSection(e);
        }

        // ===================================================================================== []
        // Successful Status Description
        private static string SuccessfulStatusDescription(StatusCode code)
        {
            return DescriptionElement("Code", code);
        }

        // --------------------------------------------------------[]
        private static string SuccessfulStatusDescription(StatusCode code, object result)
        {
            return SuccessfulStatusDescription(code)
                   + DescriptionElement("Result", result);
        }

        // --------------------------------------------------------[]
        private static string SuccessfulStatusDescription(StatusCode code, object result, string details)
        {
            return SuccessfulStatusDescription(code, result)
                   + DescriptionElement("Details", details);
        }

        // ===================================================================================== []
        // Sections & Elements
        private static string DescriptionElement(string name, object value)
        {
            return "\n{0}:[{1}]".SafeFormat(name, value);
        }

        // --------------------------------------------------------[]
        private static string ExceptionSection(Exception e)
        {
            if (e.InnerException != null)
            {
                return DescriptionElement("Exception", e.Message)+
                    DescriptionElement("Inner", ExceptionSection(e.InnerException));
            }
            return DescriptionElement("Exception", e.Message);
        }
    }
}
/*Debug Trace:
vstest.executionengine.x86.exe Information: 0 : 
Code:[FindRequestFail]
Exception:[StatusCode=[FindRemoteFileFail] StatusDescription=[
Code:[FindRemoteFileFail]
Details:[Remote file [product.d0c92576-398f-48cc-aba4-7520a2ae6ded] not found in [store/product/inprocess]]]]
 
 */