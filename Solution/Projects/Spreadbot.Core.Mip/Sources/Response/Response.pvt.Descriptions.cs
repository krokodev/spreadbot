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
            return DescriptionFail(indent,
                DescriptionField("Code", code, indent)
                );
        }

        // --------------------------------------------------------[]
        private static string FailedStatusDescription(StatusCode code, string details, int indent = 0)
        {
            return DescriptionFail(indent,
                DescriptionField("Code", code, indent),
                DescriptionField("Details", details, indent)
                );
        }

        // --------------------------------------------------------[]
        private static string FailedStatusDescription(StatusCode code, Exception e, int indent = 0)
        {
            return DescriptionFail(indent,
                DescriptionField("Code", code, indent),
                DescriptionException(e, indent)
                );
        }

        // ===================================================================================== []
        // Successful Status Description
        private static string SuccessfulStatusDescription(StatusCode code, int indent = 0)
        {
            return DescriptionSuccess(indent,
               DescriptionField("Code", code, indent)
               );
        }

        // --------------------------------------------------------[]
        private static string SuccessfulStatusDescription(StatusCode code, object result, int indent = 0)
        {
            return DescriptionSuccess(indent,
               DescriptionField("Code", code, indent),
               DescriptionField("Result", result, indent)
               );
        }

        // --------------------------------------------------------[]
        private static string SuccessfulStatusDescription(StatusCode code, object result, string details, int indent = 0)
        {
            return DescriptionSuccess(indent,
               DescriptionField("Code", code, indent),
               DescriptionField("Result", result, indent),
               DescriptionField("Details", details, indent)
               );
        }

        // ===================================================================================== []
        // Elements
        private static string DescriptionField(string name, object value, int indent = 0)
        {
            return "{0}{1}: [{2}]".SafeFormat(DescriptionIndent(indent + 1), name, value);
        }

        // --------------------------------------------------------[]
        private static string DescriptionException(Exception e, int indent = 0)
        {
            indent += 1;
            return e == null
                ? ""
                : DescriptionSection("Exception", indent,
                    DescriptionField("Type", e.GetType(), indent),
                    DescriptionField("Message", e.Message, indent),
                    DescriptionField("Inner", DescriptionException(e.InnerException, indent + 1), indent)
                    );
        }

        // --------------------------------------------------------[]
        private static string DescriptionSection(string sectionName, int indent = 0, params string[] args)
        {
            var sectionContent = "";
            args.ForEach(arg =>
            {
                sectionContent = "{0}{1}".SafeFormat(
                    sectionContent,
                    arg
                    );
            });

            return "{0}{1}:{0}[{2}{0}]".SafeFormat(
                DescriptionIndent(indent),
                sectionName,
                sectionContent
                );
        }

        // --------------------------------------------------------[]
        private static string DescriptionFail(int indent = 0, params string[] args)
        {
            return DescriptionSection("Response.Fail", indent, args);
        }
        // --------------------------------------------------------[]
        private static string DescriptionSuccess(int indent = 0, params string[] args)
        {
            return DescriptionSection("Response.Success", indent, args);
        }
        // --------------------------------------------------------[]
        private static string DescriptionIndent(int indent)
        {
            return "\n" + (indent == 0 ? "" : new string(' ', 2*indent));
        }
    }
}