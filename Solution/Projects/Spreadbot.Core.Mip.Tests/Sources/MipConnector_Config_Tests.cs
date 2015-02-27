using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Core.Mip.Tests
{
    [TestClass]
    public class MipConnector_Config_Tests
    {
        [TestMethod]
        public void Read_Mip_Config()
        {
            var configuration = MipConfiguration.Instance;
            Assert.AreEqual("mip.ebay.com", configuration.Connection.HostName);
            Assert.AreEqual(22, configuration.Connection.PortNumber);
        }

        [TestMethod]
        public void Read_Mip_Security_Config()
        {
            var configuration = MipSecurityConfiguration.Instance;
            Assert.AreEqual("cyfir", configuration.SecretData.UserName);
        }
    }
}