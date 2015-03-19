// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// SpreadbotException.cs
// romak_000, 2015-03-19 15:49

using System;
using Crocodev.Common.Extensions;

namespace Spreadbot.Sdk.Common.Exceptions
{
    public class SpreadbotException : Exception
    {
        protected SpreadbotException()
        {
        }


        public SpreadbotException(string template, params object[] args)
            : base(template.SafeFormat(args))
        {
        }
    }
}