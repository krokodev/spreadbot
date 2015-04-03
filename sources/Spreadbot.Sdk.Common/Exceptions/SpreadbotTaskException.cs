// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// SpreadbotTaskException.cs
// Roman, 2015-04-03 1:45 PM

namespace Spreadbot.Sdk.Common.Exceptions

{
    public class SpreadbotTaskException : SpreadbotException
    {
        public SpreadbotTaskException() {}

        public SpreadbotTaskException( string template, params object[] args )
            : base( string.Format( template, args ) ) {}
    }
}