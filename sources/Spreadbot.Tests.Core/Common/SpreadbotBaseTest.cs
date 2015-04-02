// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// SpreadbotBaseTest.cs
// Roman, 2015-04-01 9:11 PM

using System.Globalization;
using System.Threading;
using Crocodev.Common.Extensions;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;

namespace Spreadbot.Tests.Core.Common
{
    public class SpreadbotBaseTest
    {
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
            Assert_Inconclusive_if_Text_Contains_Message( dump, MipConnector.MipQueueDepthErrorMessage );
        }

        protected static void Assert_That_Text_Contains( object text, object fragment )
        {
            Assert.That( text.ToString()
                .Contains( fragment.ToString() ),
                "Contains '{0}'".SafeFormat( fragment.ToString() ) );
        }
    }
}