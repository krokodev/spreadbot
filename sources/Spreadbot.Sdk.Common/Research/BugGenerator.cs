// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// BugGenerator.cs
// Roman, 2015-04-09 5:56 PM

using Spreadbot.Sdk.Common.Exceptions;

// Code: BugGenerator
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