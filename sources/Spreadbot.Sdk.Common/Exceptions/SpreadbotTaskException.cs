// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.System
// SpreadbotTaskException.cs
// Roman, 2015-04-01 1:20 PM

namespace Spreadbot.Sdk.Common.Exceptions

{
    public class SpreadbotTaskException : SpreadbotException
    {
        public SpreadbotTaskException() {}

        public SpreadbotTaskException( string template, params object[] args )
            : base( string.Format( template, args ) ) {}
    }
}