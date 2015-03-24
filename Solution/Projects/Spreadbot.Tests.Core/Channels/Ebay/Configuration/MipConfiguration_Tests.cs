// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConfiguration_Tests.cs
// romak_000, 2015-03-24 11:27

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Configuration.Sections;
using Spreadbot.Core.Channels.Ebay.Mip.Settings;
using Assert = NUnit.Framework.Assert;

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

        [TestMethod]
        public void Mip_TimeZone()
        {
            //var mipNow = TimeZoneInfo.ConvertTimeBySystemTimeZoneId( DateTime.UtcNow, MipSettings.TimeZone );

            Assert.That( MipSettings.TimeZone, Is.EqualTo( "Mountain Standard Time" ) );
        }
    }
}