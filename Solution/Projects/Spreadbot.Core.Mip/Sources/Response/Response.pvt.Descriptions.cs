﻿using System;
using Crocodev.Common;
using MoreLinq;

namespace Spreadbot.Core.Mip
{
    public partial class Response<T>
    {
        // ===================================================================================== []
        // Get Description
        private string GetSuccessDescription(int level)
        {
            return DescriptionSuccess(level++,
                DescriptionField("Code", Code, level),
                DescriptionField("Result", Result, level),
                DescriptionField("Details", Details, level),
                DescriptionInnerResponse(InnerResponse, level)
                );
        }

        // --------------------------------------------------------[]
        private string GetFailedDescription(int level)
        {
            return DescriptionFail(level++,
                DescriptionField("Code", Code, level),
                DescriptionException(Exception, level),
                DescriptionField("Details", Details, level),
                DescriptionInnerResponse(InnerResponse, level)
                );
        }

        // ===================================================================================== []
        // Elements
        private static string DescriptionField(string name, object value, int level)
        {
            if (value == null)
                return "";

            return "{0}{1}: [{2}]".SafeFormat(NewLine(level), name, value);
        }

        // --------------------------------------------------------[]
        private static string DescriptionException(Exception e, int level)
        {
            if (e == null)
                return "";

            return DescriptionSection("Exception", level++,
                DescriptionField("Type", e.GetType(), level),
                DescriptionField("Message", ExceptionMessage(e, level), level),
                DescriptionField("InnerException", DescriptionException(e.InnerException, level), level)
                );
        }

        private static string ExceptionMessage(Exception e, int level)
        {
            var responseException = e as ResponseException;
            if (responseException != null)
            {
                return responseException.Response.GetDescription(level);
            }
            return e.Message;
        }

        // --------------------------------------------------------[]
        private static string DescriptionInnerResponse(IResponse response, int level)
        {
            if (response == null)
                return "";

            return DescriptionSection("InnerResponse", level++,
                response.GetDescription(level)
                );
        }

        // --------------------------------------------------------[]
        private static string DescriptionSection(string sectionName, int level = 0, params string[] args)
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
        private static string DescriptionFail(int level = 0, params string[] args)
        {
            return DescriptionSection("Response.Fail", level, args);
        }

        // --------------------------------------------------------[]
        private static string DescriptionSuccess(int level = 0, params string[] args)
        {
            return DescriptionSection("Response.Success", level, args);
        }

        // --------------------------------------------------------[]
        private static string NewLine(int level)
        {
            return "\n" + (level == 0 ? "" : new string(' ', 2*level));
        }
    }
}