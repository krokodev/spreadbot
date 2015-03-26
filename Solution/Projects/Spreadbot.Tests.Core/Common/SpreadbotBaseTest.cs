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