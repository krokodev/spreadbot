using System;
using Crocodev.Common;

namespace Spreadbot.Core.Common
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