using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Core.Channel.Ebay.Mip.Tests
{
    [TestClass]
    public class MipConfiguration_Tests
    {
        [TestMethod]
        public void Read_Mip_Config()
        {
            var configuration = Configuration.MipPublicConfig.Instance;
            Assert.AreEqual("mip.ebay.com", configuration.MipConnection.HostName);
            Assert.AreEqual(22, configuration.MipConnection.PortNumber);
        }

        [TestMethod]
        public void Read_Mip_Security_Config()
        {
            var configuration = Configuration.MipSecurityConfig.Instance;
            Assert.AreEqual("cyfir", configuration.MipSecretData.UserName);
        }
    }
}