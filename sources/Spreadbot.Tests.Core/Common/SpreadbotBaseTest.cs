// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// SpreadbotBaseTest.cs
// romak_000, 2015-03-26 19:42

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