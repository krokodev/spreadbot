// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit
// Spreadbot_Tests.cs

using System.Globalization;
using System.Threading;
using Krokodev.Common.Extensions;
using NUnit.Framework;

namespace Spreadbot.Nunit.Base
{
    public class Spreadbot_Tests
    {
        protected void InitCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
        }

        protected static void Assert_Inconclusive_if_Text_Contains_Message(
            object text,
            string message,
            string comment = "" )
        {
            if( text.ToString().Contains( message ) ) {
                Assert.Inconclusive( "Can't concluse the test due to [{0}] {1}".SafeFormat( message, comment ) );
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