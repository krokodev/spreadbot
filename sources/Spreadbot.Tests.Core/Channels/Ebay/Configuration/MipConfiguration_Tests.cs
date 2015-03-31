// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConfiguration_Tests.cs
// Roman, 2015-03-31 1:27 PM

using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Configuration.Sections;
using Spreadbot.Core.Channels.Ebay.Mip.Settings;
using Spreadbot.Tests.Core.Common;

namespace Spreadbot.Tests.Core.Channels.Ebay.Configuration
{
    [TestFixture]
    public class MipConfiguration_Tests : SpreadbotBaseTest
    {
        [Test]
        public void Read_Mip_Config()
        {
            var configuration = MipPublicConfig.Instance;
            Assert.AreEqual( "mip.ebay.com", configuration.MipConnection.HostName );
            Assert.AreEqual( 22, configuration.MipConnection.PortNumber );
        }

        [Test]
        public void Read_Mip_Security_Config()
        {
            var configuration = MipSecurityConfig.Instance;
            Assert.AreEqual( "cyfir", configuration.MipSecretData.UserName );
        }

        [Test]
        public void Mip_TimeZone()
        {
            //var mipNow = TimeZoneInfo.ConvertTimeBySystemTimeZoneId( DateTime.UtcNow, MipSettings.TimeZone );

            Assert.That( MipSettings.TimeZone, Is.EqualTo( "Mountain Standard Time" ) );
        }
    }
}