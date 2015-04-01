// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// SpreadbotBaseTest.cs
// Roman, 2015-03-31 1:27 PM

using System.Globalization;
using System.Threading;
using Crocodev.Common.Extensions;
using NUnit.Framework;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Tests.Core.Common
{
    public class SpreadbotBaseTest
    {
        protected const string MipQueueDepthErrorMessage = "Error message from server: Exceeded the Queue Depth of 10";


        [SetUp]
        public void InitCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
        }

        protected static void Assert_Inconclusive_If_Exception_Contains( SpreadbotException exception, string message )
        {
            if( exception.Message.Contains( message ) ) {
                Assert.Inconclusive( "Can't concluse the test due to [{0}]".SafeFormat( message ) );
            }
        }
    }
}