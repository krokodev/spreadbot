using System;
using Crocodev.Common;
using MoreLinq;

namespace Spreadbot.Core.Mip
{
    public partial class Response
    {
        // ===================================================================================== []
        // Failed Status Description
        private static string FailedStatusDescription(StatusCode code, int indent = 0)
        {
            return DescriptionHeader("Response fail", indent)
                + DescriptionField("Code", code, indent);
        }

        // --------------------------------------------------------[]
        private static string FailedStatusDescription(StatusCode code, string details, int indent = 0)
        {
            return FailedStatusDescription(code, indent)
                   + DescriptionField("Details", details, indent);
        }

        // --------------------------------------------------------[]
        private static string FailedStatusDescription(StatusCode code, Exception e, int indent = 0)
        {
            return FailedStatusDescription(code, indent)
                   + DescriptionException(e, indent);
        }

        // ===================================================================================== []
        // Successful Status Description
        private static string SuccessfulStatusDescription(StatusCode code, int indent = 0)
        {
            return DescriptionHeader("Response success", indent)
                + DescriptionField("Code", code, indent);
        }


        // --------------------------------------------------------[]
        private static string SuccessfulStatusDescription(StatusCode code, object result, int indent = 0)
        {
            return SuccessfulStatusDescription(code, indent)
                   + DescriptionField("Result", result, indent);
        }

        // --------------------------------------------------------[]
        private static string SuccessfulStatusDescription(StatusCode code, object result, string details, int indent=0)
        {
            return SuccessfulStatusDescription(code, result, indent)
                   + DescriptionField("Details", details, indent);
        }

        // ===================================================================================== []
        // Elements
        private static string DescriptionField(string name, object value, int indent = 0)
        {
            return "\n{0}{1}:[{2}]".SafeFormat(DescriptionIndent(indent + 1), name, value);
        }

        // --------------------------------------------------------[]
        private static string DescriptionHeader(string header, int indent)
        {
            return "\n{0}".SafeFormat(header, indent);
        }

        // --------------------------------------------------------[]
        private static string DescriptionException(Exception e, int indent = 0)
        {
            indent += 1;
            return e == null
                ? ""
                : DescriptionSection("Exception", indent,
                    DescriptionField("Message", e.Message),
                    DescriptionField("Inner", DescriptionException(e.InnerException, indent + 1))
                    );
        }

        // --------------------------------------------------------[]
        private static string DescriptionSection(string name, int indent = 0, params string[] args)
        {
            var res = "\n{0}{1}".SafeFormat(
                DescriptionIndent(indent),
                name
                );

            args.ForEach(s =>
            {
                res = "{0}\n{1}{2}".SafeFormat(
                    res,
                    DescriptionIndent(indent + 1),
                    s
                    );
            });
            return res;
        }

        // --------------------------------------------------------[]
        private static string DescriptionIndent(int indent)
        {
            return indent == 0 ? "" : new string(' ', 2*indent);
        }
    }
}