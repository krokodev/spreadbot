// Spreadbot (c) 2015 Crocodev
// Tests.Common
// SpreadbotTestBase.cs
// Roman, 2015-04-03 8:17 PM

using System.Globalization;
using System.Threading;
using Crocodev.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;

namespace Tests.Common
{
    public class SpreadbotTestBase
    {
        protected void InitCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
        }

        protected static void Assert_Inconclusive_if_Text_Contains_Message( string text, string message )
        {
            if( text.Contains( message ) ) {
                Assert.Inconclusive( "Can't concluse the test due to [{0}]".SafeFormat( message ) );
            }
        }

        protected static void IgnoreMipQueueDepthErrorMessage( object obj )
        {
            Assert_Inconclusive_if_Text_Contains_Message( obj.ToString(), MipConnector.MipQueueDepthErrorMessage );
        }

        protected static void Assert_That_Text_Contains( object text, object fragment )
        {
            Assert.IsTrue( text.ToString()
                .Contains( fragment.ToString() ),
                "Contains '{0}'".SafeFormat( fragment.ToString() ) );
        }
    }
}