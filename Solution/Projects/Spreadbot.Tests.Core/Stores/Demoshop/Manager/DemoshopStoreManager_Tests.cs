// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// DemoshopStoreManager_Tests.cs
// romak_000, 2015-03-20 23:57

using System.Diagnostics;
using System.Linq;
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

            Assert.AreEqual( TaskStatus.Todo, task.GetStatusCode() );
            Assert.IsNull( task.AbstractResponse );
            task.AbstractSubTasks.ForEach( ct => { Assert.IsTrue( ct.IsCritical ); } );
            store.DeleteTask( task );
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void Run_Task_PublishItemOnEbay()
        {
            var store = DemoshopStoreManager.Instance;
            var task = store.CreateTask( DemoshopStoreTaskType.PublishOnEbay );
            Dispatcher.Instance.RunChannelTasks( store.GetChannelTasks() );

            Assert.AreEqual( TaskStatus.Inprocess, task.GetStatusCode() );

            task.AbstractSubTasks.OfType< EbayPublishTask >().ForEach( t => {
                Trace.Write( t );
                Assert.AreEqual( TaskStatus.Inprocess, t.GetStatusCode() );
                Assert.IsNotNull( t.EbayPublishResponse.Result.MipRequestId );
            } );
            store.DeleteTask( task );
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void Proceed_Task_PublishItemOnEbay()
        {
            var store = DemoshopStoreManager.Instance;
            var task = store.CreateTask( DemoshopStoreTaskType.PublishOnEbay );
            Dispatcher.Instance.RunChannelTasks( store.GetChannelTasks() );
            Dispatcher.Instance.ProceedChannelTasks( store.GetChannelTasks() );

            Assert.AreEqual( TaskStatus.Inprocess, task.GetStatusCode() );

            task.AbstractSubTasks.OfType< EbayPublishTask >().ForEach( t => {
                Trace.Write( t );
                Assert.IsTrue( t.GetStatusCode() == TaskStatus.Inprocess || t.GetStatusCode() == TaskStatus.Success );
                Assert.IsNotNull( t.EbayPublishResponse.Result.MipRequestId );
            } );
            store.DeleteTask( task );
        }

        // --------------------------------------------------------[]
        [Ignore]
        [TestMethod]
        public void SaveRestore_Proceed_Task_PublishItemOnEbay()
        {
            // Todo: Create_Run_SaveRestore_Proceed_Task_PublishItemOnEbay
        }
    }
}