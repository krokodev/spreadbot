// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// GenericResponse.pvt.Descriptions.cs
// romak_000, 2015-03-19 15:38

using System;
using Crocodev.Common.Extensions;
using MoreLinq;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Sdk.Common.Operations.Responses
{
    public partial class GenericResponse<TR, TC>
    {
        // ===================================================================================== []
        // Ctror
        protected GenericResponse()
        {
        }

        // ===================================================================================== []
        // Get Autoinfo
        private string GetSuccessAutoinfo(int level)
        {
            return AutoinfoResponse(level++,
                AutoinfoField("Result", Result, level),
                AutoinfoField("Details", Details, level),
                AutoinfoInnerResponse(InnerResponse, level)
                );
        }

        // --------------------------------------------------------[]
        private string GetFailedAutoinfo(int level)
        {
            return AutoinfoResponse(level++,
                AutoinfoException(Exception, level),
                AutoinfoField("Details", Details, level),
                AutoinfoInnerResponse(InnerResponse, level)
                );
        }


        // ===================================================================================== []
        // Elements
        private static string AutoinfoField(string name, object value, int level)
        {
            if (value == null)
                return "";

            return "{0}{1}: [{2}]".SafeFormat(NewLine(level), name, value);
        }

        // --------------------------------------------------------[]
        private static string AutoinfoException(Exception e, int level)
        {
            if (e == null)
                return null;

            return AutoinfoSection("Exception", level++,
                AutoinfoField("Type", e.GetType(), level),
                AutoinfoField("Message", ExceptionMessage(e, level + 1), level),
                AutoinfoField("InnerException", AutoinfoException(e.InnerException, level), level)
                );
        }

        private static string ExceptionMessage(Exception e, int level)
        {
            var responseException = e as ResponseException;
            if (responseException != null)
            {
                return responseException.Response.GetAutoinfo(level);
            }
            return e.Message;
        }

        // --------------------------------------------------------[]
        private static string AutoinfoInnerResponse(IResponse response, int level)
        {
            if (response == null)
                return null;

            return AutoinfoSection("InnerResponse", level++,
                response.GetAutoinfo(level)
                );
        }

        // --------------------------------------------------------[]
        private static string AutoinfoSection(string sectionName, int level = 0, params string[] args)
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
                NewLine(level),
                sectionName,
                sectionContent
                );
        }

        // --------------------------------------------------------[]
        private string AutoinfoResponse(int level = 0, params string[] args)
        {
            return AutoinfoSection(Code.ToString(), level, args);
        }

        // --------------------------------------------------------[]
        private static string NewLine(int level)
        {
            return "\n" + (level == 0 ? "" : new string(' ', 2*level));
        }

        // ===================================================================================== []
        // Object
        public override string ToString()
        {
            return Autoinfo;
        }
    }
}