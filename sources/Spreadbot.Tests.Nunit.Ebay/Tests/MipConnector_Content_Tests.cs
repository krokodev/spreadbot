// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Ebay
// MipConnector_Content_Tests.cs

using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Configuration.Sections;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Nunit.Ebay.Base;
using Spreadbot.Nunit.Ebay.Utils;

namespace Spreadbot.Nunit.Ebay.Tests
{
    [TestFixture]
    public partial class MipConnectorContent_Tests : Ebay_Tests
    {
        // --------------------------------------------------------[]
        [SetUp]
        public void InitMethod()
        {
            MipConnectorTestInitializer.PrepareTestFiles();
        }

        // --------------------------------------------------------[]
        [Test]
        public void Read_ItemId()
        {
            _TestReadItemId( MipFeedType.Distribution );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Read_Mip_Config()
        {
            var configuration = EbayPublicConfig.Instance;
            Assert.AreEqual( "mip.ebay.com", configuration.MipConnection.HostName );
            Assert.AreEqual( 22, configuration.MipConnection.PortNumber );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Read_Product_Content()
        {
            _TestReadItemId( MipFeedType.Product );
            _TestReadAllFeedStatuses( MipFeedType.Product );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Read_Availability_Content()
        {
            _TestReadAllFeedStatuses( MipFeedType.Availability );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Read_Distribution_Content()
        {
            _TestReadItemId( MipFeedType.Distribution );
            _TestReadAllFeedStatuses( MipFeedType.Distribution );
        }
    }
}