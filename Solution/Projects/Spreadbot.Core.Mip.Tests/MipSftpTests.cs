using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Core.Mip.Tests
{
    [TestClass]
    public class MipSftpTests
    {
        [TestMethod]
        public void Upload_Feed_On_MIP()
        {
            var feed = new MipFeed(MipFeedType.Product);
            var settings = new MipSettings();
            var mip = new MipConnector(settings);
            var response = mip.UploadFeed(feed);

            Assert.AreEqual(MipStatusCode.CommandOk, response.StatusCode);
        }
    }
}
