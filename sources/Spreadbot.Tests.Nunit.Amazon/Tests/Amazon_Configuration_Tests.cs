// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Amazon_Configuration_Tests.cs

using NUnit.Framework;
using Spreadbot.Nunit.Amazon.Base;

namespace Spreadbot.Nunit.Amazon.Tests
{
    [TestFixture]
    public class Amazon_Configuration_Tests : Amazom_Tests
    {
        [Test]
        public void Read_Amazon_Config()
        {
            Assert.Inconclusive();

/*            var configuration = AmazonPublicConfig.Instance;
            Assert.AreEqual( "mip.ebay.com", configuration.MipConnection.HostName );
            Assert.AreEqual( 22, configuration.MipConnection.PortNumber );*/
        }

        [Test]
        public void Read_Amazon_Security_Config()
        {
            Assert.Inconclusive();

            /*
            var configuration = EbaySecretConfig.Instance;
            Assert.AreEqual( "cyfir", configuration.MipSecretData.UserName );
*/
        }
    }
}