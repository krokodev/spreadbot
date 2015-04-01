// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// SpreadbotBaseTest.cs
// Roman, 2015-04-01 4:59 PM

using System.Globalization;
using System.Threading;
using Crocodev.Common.Extensions;
using NUnit.Framework;

namespace Spreadbot.Tests.Core.Common
{
    public class SpreadbotBaseTest
    {
        protected const string MipQueueDepthErrorMessage = "Exceeded the Queue Depth of 10";

        [SetUp]
        public void InitCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
        }

        private static void Assert_Inconclusive_if_Text_Contains_Message( string text, string message )
        {
            if( text.Contains( message ) ) {
                Assert.Inconclusive( "Can't concluse the test due to [{0}]".SafeFormat( message ) );
            }
        }

        protected static void IgnoreMipQueueDepthErrorMessage( string dump )
        {
            Assert_Inconclusive_if_Text_Contains_Message( dump, MipQueueDepthErrorMessage );
        }
    }
}