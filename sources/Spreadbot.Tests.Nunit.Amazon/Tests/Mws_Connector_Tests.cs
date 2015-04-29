// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Mws_Adapter_Tests.cs

using System;
using NUnit.Framework;
using Spreadbot.Core.Channels.Amazon.Mws.Connector;
using Spreadbot.Core.Channels.Amazon.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Mws.Operations.Request;
using Spreadbot.Core.Channels.Amazon.Mws.Operations.StatusCode;
using Spreadbot.Nunit.Amazon.Base;

namespace Spreadbot.Nunit.Amazon.Tests
{
    [TestFixture]
    public class Mws_Connector_Tests : Amazon_Tests
    {
        [SetUp]
        public static void Init() {}

        [Test]
        public void Submit_Product_Feed()
        {
            var feed = new MwsFeedHandler( MwsFeedType.Product );

            var response = MwsConnector.Instance.SubmitFeed( feed );
            Console.WriteLine( response );
            //IgnoreMipQueueDepthErrorMessage( response );

            Assert.AreEqual( MwsOperationStatus.SubmitFeedSuccess, response.StatusCode );
            Assert.IsTrue( MwsRequestHandler.VerifyRequestId( response.Result.MwsRequestId ) );
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