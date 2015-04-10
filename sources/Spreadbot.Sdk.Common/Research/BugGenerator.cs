// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// BugGenerator.cs
// Roman, 2015-04-10 1:28 PM

using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Sdk.Common.Research
{
    public class BugGenerator
    {
        public static void ThrowSpreadbotException( string message )
        {
            throw new SpreadbotException( message );
        }
    }
}