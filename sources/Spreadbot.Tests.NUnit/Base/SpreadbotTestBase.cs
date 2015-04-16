// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Nunit
// SpreadbotTestBase.cs

using System.Globalization;
using System.Threading;
using Krokodev.Common.Extensions;
using NUnit.Framework;

namespace Spreadbot.Nunit.Base
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

        protected static void Assert_That_Text_Contains( object text, object fragment )
        {
            Assert.IsTrue( text.ToString()
                .Contains( fragment.ToString() ),
                "Contains '{0}'".SafeFormat( fragment.ToString() ) );
        }
    }
}