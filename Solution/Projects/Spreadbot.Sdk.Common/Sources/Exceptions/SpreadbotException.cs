using System;
using Crocodev.Common;

namespace Spreadbot.Sdk.Common
{
    public class SpreadbotException : Exception
    {
        protected SpreadbotException()
        {
            
        }
        public SpreadbotException(string template, params object[] args)
            :base(template.SafeFormat(args))
        {
        }
    }
}