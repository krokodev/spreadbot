// Spreadbot (c) 2015 Crocodev
// Tests.MSTest
// MipConnector_Content_Tests.cs
// Roman, 2015-04-06 1:15 PM

using System;
using Crocodev.Common.Extensions;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadbot.Core.Channels.Ebay.Configuration.Sections;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Connector.Fakes;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Tests.Common;

namespace Tests.MSTest.Units
{
    [TestClass]
    public class MipConnector_Content_Tests : SpreadbotTestBase
    {
        // --------------------------------------------------------[]
        [ClassInitialize]
        [DeploymentItem( @"App_Data\", "App_Data" )]
        public static void InitClass( TestContext context ) {}

        // --------------------------------------------------------[]
        [TestInitialize]
        public void InitMethod()
        {
            MipConnectorTestInitializer.PrepareTestFiles();
        }

        // --------------------------------------------------------[]
        private static void DoTestItemId( MipFeedType mipFeedType )
        {
            var feed = new MipFeedHandler( mipFeedType );
            var request = new MipRequestHandler( feed, MipConnectorTestInitializer.ItemRequestId );

            var requestResponse = MipConnector.GetRequestStatus( request );
            Console.WriteLine( requestResponse );

            Assert.IsNotNull( requestResponse.Result );
            Assert.AreEqual( MipConnectorTestInitializer.ProductItemId,
                requestResponse.Result.MipItemId,
                "{0}.ItemId".SafeFormat( feed.Type ) );
        }

        // --------------------------------------------------------[]
        private static void TestItemId( MipFeedType mipFeedType )
        {
            using( ShimsContext.Create() ) {
                ShimMipConnector.FindRequestIn_InprocessMipRequestHandler =
                    mipRequestHandler => new MipResponse< MipFindRemoteFileResult > {
                        IsSuccess = false,
                        StatusCode = MipOperationStatus.FindRemoteFileFailure,
                        Details = "Fake"
                    };

                ShimMipConnector.FindRequestIn_OutputMipRequestHandler =
                    mipRequestHandler => new MipResponse< MipFindRemoteFileResult > {
                        IsSuccess = true,
                        StatusCode = MipOperationStatus.FindRemoteFileSuccess,
                        Result = new MipFindRemoteFileResult {
                            RemoteDir = "fake/remote/dir",
                            RemoteFileName = mipRequestHandler.FileNamePrefix() + ".xml"
                        },
                        Details = "Fake"
                    };

                ShimMipConnector.ShimSftpHelper.DoDownloadFilesStringString = ( from, to ) => { };

                DoTestItemId( mipFeedType );
            }
        }

        // --------------------------------------------------------[]
         [TestMethod]
      //  [DeploymentItem( @"App_Data\", "App_Data" )]
        public void Read_ItemId()
        {
            TestItemId( MipFeedType.Distribution );
        }

        // --------------------------------------------------------[]
        [TestMethod]
        //[DeploymentItem( @"App_Data\", "App_Data" )]
        public void Read_Mip_Config()
        {
            var configuration = MipPublicConfig.Instance;
            Assert.AreEqual( "mip.ebay.com", configuration.MipConnection.HostName );
            Assert.AreEqual( 22, configuration.MipConnection.PortNumber );
        }

        // --------------------------------------------------------[]
        private static void TestFeedStatus( MipFeedType mipFeedType, MipRequestStatus mipRequestStatus )
        {
            /*            var wasTested = false;
            var feed = new MipFeedHandler( mipFeedType );

            MipConnectorTestInitializer.TestRequestIds( feed.Type, mipRequestStatus )
                .ForEach( reqId => {
                    var request = new MipRequestHandler( feed, reqId );
                    var requestResponse = MipConnector.Mock_GetRequestStatus( request );
                    var testInfo = "{0}.{1} checking status".SafeFormat( feed.Type, reqId );
                    wasTested = true;

                    Assert.AreEqual( MipOperationStatus.GetRequestStatusSuccess, requestResponse.StatusCode, testInfo );
                    Assert.AreEqual( mipRequestStatus, requestResponse.Result.MipRequestStatusCode, testInfo );
                } );

            Assert.AreEqual( true, wasTested, "{0}.{1} was not tested".SafeFormat( mipFeedType, mipRequestStatus ) );*/

            Assert.Fail();
        }

        // --------------------------------------------------------[]
        /*        private static void TestAllFeedStatuses( MipFeedType mipFeedType )
        {
            TestFeedStatus( mipFeedType, MipRequestStatus.Success );
            TestFeedStatus( mipFeedType, MipRequestStatus.Failure );
            TestFeedStatus( mipFeedType, MipRequestStatus.Unknown );
        }

        // --------------------------------------------------------[]
        // Code: Use Fakes

         * [NUnit.Framework.Ignore( "Waiting for Fakes" )]
        [Test]
        public void Read_Product_Content()
        {
            TestItemId( MipFeedType.Product );
            TestAllFeedStatuses( MipFeedType.Product );
        }

        // --------------------------------------------------------[]
        [NUnit.Framework.Ignore( "Waiting for Fakes" )]
        [Test]
        public void Read_Availability_Content()
        {
            TestAllFeedStatuses( MipFeedType.Availability );
        }

        // --------------------------------------------------------[]
        [NUnit.Framework.Ignore( "Waiting for Fakes" )]
        [Test]
        public void Read_Distribution_Content()
        {
            TestItemId( MipFeedType.Distribution );
            TestAllFeedStatuses( MipFeedType.Distribution );
        }*/
    }
}