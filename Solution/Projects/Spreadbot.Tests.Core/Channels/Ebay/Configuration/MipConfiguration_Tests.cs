// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConfiguration_Tests.cs
// romak_000, 2015-03-23 15:37

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadbot.Core.Channels.Ebay.Configuration.Sections;

namespace Spreadbot.Tests.Core.Channels.Ebay.Configuration
{
    [TestClass]
    public class MipConfiguration_Tests
    {
        [TestMethod]
        public void Read_Mip_Config()
        {
            var configuration = MipPublicConfig.Instance;
            Assert.AreEqual( "mip.ebay.com", configuration.MipConnection.HostName );
            Assert.AreEqual( 22, configuration.MipConnection.PortNumber );
        }

        [TestMethod]
        public void Read_Mip_Security_Config()
        {
            var configuration = MipSecurityConfig.Instance;
            Assert.AreEqual( "cyfir", configuration.MipSecretData.UserName );
        }
    }
}