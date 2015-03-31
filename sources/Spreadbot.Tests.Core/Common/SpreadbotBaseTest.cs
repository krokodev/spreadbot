// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// SpreadbotBaseTest.cs
// Roman, 2015-03-31 1:27 PM

using System.Globalization;
using System.Threading;
using NUnit.Framework;

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
    }
}