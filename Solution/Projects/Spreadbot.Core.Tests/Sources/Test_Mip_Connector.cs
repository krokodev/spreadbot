using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Core.Mip.Tests
{
    [TestClass]
    public class Test_Mip_Connector
    {
        [TestMethod]
        public void Zip_And_Send_Feed_To_MIP()
        {
            var feed = new Feed(FeedType.Product);
            var response = Connector.SendFeed(feed);

            Trace.TraceInformation("StatusDescription=[{0}]", response.StatusDescription);

            Assert.AreEqual(StatusCode.SendFeedSuccess, response.StatusCode);
            Assert.IsTrue(Request.VerifyRequestId(response.RequestId));
        }

        [TestMethod]
        public void Find_Unknown_Request()
        {
            var feed = new Feed(FeedType.Product);
            var request = new Request(feed, Request.GenerateId());
            Response findResponce = Connector.FindRequest(request, RequestProcessingStage.Inprocess);

            Assert.AreEqual(StatusCode.FindRequestSuccess, findResponce.StatusCode);
            Assert.AreEqual(FindRequestResult.NotFound, (FindRequestResult)findResponce.Result);
        }


        [TestMethod]
        public void Find_Wrong_Request()
        {
            var feed = new Feed(FeedType.None);
            var request = new Request(feed, Request.GenerateId());
            Response findResponce = Connector.FindRequest(request, RequestProcessingStage.Inprocess);

            Assert.AreEqual(StatusCode.FindRequestFail, findResponce.StatusCode);
            Assert.AreEqual(FindRequestResult.Error, (FindRequestResult)findResponce.Result);
        }


        [TestMethod]
        public void Find_Newly_Generated_Request()
        {
            // Now: Find_Newly_Generated_Request

            var feed = new Feed(FeedType.Product);
            var sendResponse = Connector.SendFeed(feed);
            var request = new Request(feed, sendResponse.RequestId);
            Response findResponce = Connector.FindRequest(request, RequestProcessingStage.Inprocess);

            Assert.AreEqual(StatusCode.FindRequestSuccess, findResponce.StatusCode);
            Assert.AreEqual(FindRequestResult.Found, (FindRequestResult)findResponce.Result);
        }

        [TestMethod]
        public void Check_Request_Status_Output()
        {
            throw new AssertFailedException("Not implemented");
            // Now: Check_Request_Status_Output
            /*
                        var feed = new Feed(FeedType.Product);
                        var response = Connector.CheckRequest();
            */
        }
    }
}