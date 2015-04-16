// Spreadbot (c) 2015 Crocodev
// Tests.NUnit
// MipConnector_Content_Tests.cs

using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Configuration.Sections;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Tests.NUnit.Code;

namespace Tests.NUnit.Units
{
    [TestFixture]
    public partial class MipConnector_Content_Tests : SpreadbotTestBase
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
            var configuration = MipPublicConfig.Instance;
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