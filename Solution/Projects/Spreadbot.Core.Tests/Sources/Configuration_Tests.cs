using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Core.Channel.Ebay.Mip.Tests
{
    [TestClass]
    public class Configuration_Tests
    {
        [TestMethod]
        public void Read_Mip_Config()
        {
            var configuration = Configuration.MipPublic.Instance;
            Assert.AreEqual("mip.ebay.com", configuration.Connection.HostName);
            Assert.AreEqual(22, configuration.Connection.PortNumber);
        }

        [TestMethod]
        public void Read_Mip_Security_Config()
        {
            var configuration = Configuration.MipSecurity.Instance;
            Assert.AreEqual("cyfir", configuration.SecretData.UserName);
        }
    }
}