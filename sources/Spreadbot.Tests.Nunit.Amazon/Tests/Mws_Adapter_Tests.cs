// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Mws_Adapter_Tests.cs

using NUnit.Framework;
using Spreadbot.Nunit.Amazon.Base;

namespace Spreadbot.Nunit.Amazon.Tests
{
    [TestFixture]
    public class Mws_Adapter_Tests : Amazon_Tests
    {
        [SetUp]
        public static void Init() {}

        [Test]
        public void Submit_Product_Feed()
        {
            /*
            var feed = new MipFeedHandler( MipFeedType.Product );

            var response = MipConnector.Instance.SubmitFeed( feed );
            Console.WriteLine( response );
            IgnoreMipQueueDepthErrorMessage( response );

            Assert.AreEqual( MipOperationStatus.SubmitFeedSuccess, response.StatusCode );
            Assert.IsTrue( MipRequestHandler.VerifyRequestId( response.Result.MipRequestId ) );
            Assert_That_Text_Contains( response, "InnerResponses" );
            Assert_That_Text_Contains( response, MipOperationStatus.ZipFeedSuccess );
            Assert_That_Text_Contains( response, MipOperationStatus.SftpSendFilesSuccess );
*/
        }

        /*
        [Test]
        public void Find_Request_Inprocess() {}

        [Test]
        public void Dont_Find_Unknown_Request_Inprocess() {}

        [Test]
        public void Get_Request_Status_Inproc() {}

        [Test]
        public void Get_Request_Status_Success() {}

        [Test]
        public void Get_Request_Status_Unknown() {}

        [Test]
        public void Response_Contains_Args_Info() {}
*/
    }
}