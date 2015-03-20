// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// DemoshopStoreManager_Tests.cs
// romak_000, 2015-03-20 16:51

using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreLinq;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;
using Spreadbot.Core.Stores.Demoshop.Manager;
using Spreadbot.Core.Stores.Demoshop.Operations.Tasks;
using Spreadbot.Core.System.Dispatcher;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Tests.Core.Stores.Demoshop.Manager
{
    [TestClass]
    public class DemoshopStoreManager_Tests
    {
        // --------------------------------------------------------[]
        [ClassInitialize]
        public static void Init( TestContext testContext ) {}

        // --------------------------------------------------------[]
        [TestMethod]
        public void Create_Task_PublishItemOnEbay()
        {
            var store = DemoshopStoreManager.Instance;
            var task = store.CreateTask( DemoshopStoreTaskType.PublishOnEbay );

            Assert.AreEqual(TaskStatus.Todo, task.GetStatusCode());
            Assert.IsNull(task.AbstractResponse);
            task.AbstractSubTasks.ForEach(
                ct => { Assert.IsTrue( ct.IsCritical ); } );
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void Run_Task_PublishItemOnEbay()
        {
            var store = DemoshopStoreManager.Instance;
            var task = store.CreateTask( DemoshopStoreTaskType.PublishOnEbay );
            Dispatcher.Instance.RunChannelTasks( store.GetChannelTasks() );

            Assert.AreEqual( TaskStatus.Inprocess, task.GetStatusCode() );
            task.AbstractSubTasks.ForEach(
                ct => {
                    var ept = ( EbayPublishTask ) ct;
                    Assert.AreEqual( TaskStatus.Inprocess, ct.GetStatusCode() );
                    
                    Assert.IsNotNull( ct.AbstractResponse );
                    Trace.Write( ct.AbstractResponse );

                    Assert.IsNotNull( ept.EbayPublishResponse.Result.MipRequestId );
                } );
        }
    }
}