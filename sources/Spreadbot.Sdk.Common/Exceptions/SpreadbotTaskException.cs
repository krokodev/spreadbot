// Spreadbot (c) 2015 Krokodev
// Spreadbot.Sdk.Common
// SpreadbotTaskException.cs

namespace Spreadbot.Sdk.Common.Exceptions

{
    public class SpreadbotTaskException : SpreadbotException
    {
        public SpreadbotTaskException() {}

        public SpreadbotTaskException( string template, params object[] args )
            : base( string.Format( template, args ) ) {}
    }
}