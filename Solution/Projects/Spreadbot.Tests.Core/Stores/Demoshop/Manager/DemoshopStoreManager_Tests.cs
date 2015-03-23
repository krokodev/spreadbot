// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// DemoshopStoreManager_Tests.cs
// romak_000, 2015-03-23 12:34

using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreLinq;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
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
        [TestCleanup]
        public void DeleteAllStoreTasks()
        {
            DemoshopStoreManager.Instance.DeleteAllTasks();
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void Create_Task_PublishItemOnEbay()
        {
            var store = DemoshopStoreManager.Instance;
            var task = store.CreateTask( DemoshopStoreTaskType.PublishOnEbay );

            Assert.AreEqual( TaskStatus.Todo, task.GetStatusCode() );
            Assert.IsNull( task.AbstractResponse );
            task.AbstractSubTasks.ForEach( ct => { Assert.IsTrue( ct.IsCritical ); } );
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void Run_Task_PublishItemOnEbay()
        {
            var store = DemoshopStoreManager.Instance;
            var task = store.CreateTask( DemoshopStoreTaskType.PublishOnEbay );
            Dispatcher.Instance.RunChannelTasks( store.GetChannelTasks() );

            task.AbstractSubTasks.OfType< EbayPublishTask >().ForEach( t => {
                Trace.WriteLine( t.EbayPublishResponse );
                Assert.AreEqual( TaskStatus.Inprocess, t.GetStatusCode() );
                Assert.IsNotNull( t.EbayPublishResponse.Result.MipRequestId );
            } );
            Assert.AreEqual(TaskStatus.Inprocess, task.GetStatusCode());
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void Proceed_Task_PublishItemOnEbay()
        {
            var dispatcher = Dispatcher.Instance;
            var store = DemoshopStoreManager.Instance;
            var task = store.CreateTask( DemoshopStoreTaskType.PublishOnEbay );

            dispatcher.RunChannelTasks( store.GetChannelTasks() );
            dispatcher.ProceedChannelTasks( store.GetChannelTasks() );

            task.AbstractSubTasks.OfType< EbayPublishTask >().ForEach( t => {
                Trace.WriteLine(t);
                Assert.IsTrue( t.GetStatusCode() == TaskStatus.Inprocess || t.GetStatusCode() == TaskStatus.Success );
                Assert.IsNotNull( t.EbayPublishResponse.Result.MipRequestId );
            } );
            Assert.AreEqual(TaskStatus.Inprocess, task.GetStatusCode());
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void Save_and_Restore_Tasks()
        {
            var store = DemoshopStoreManager.Instance;

            store.CreateTask( DemoshopStoreTaskType.PublishOnEbay );
            store.SaveData();
            store.DeleteAllTasks();
            store.RestoreData();

            Assert.AreEqual( 1, store.StoreTasks.Count );
            Assert.AreEqual( 3, store.GetChannelTasks().Count() );

            var feeds =
                store.GetChannelTasks()
                    .OfType< EbayPublishTask >()
                    .Select( t => t.Args.MipFeedHandler.Type )
                    .OrderBy( f => f.ToString() ).ToArray();

            Assert.AreEqual( 3, feeds.Count() );
            Assert.AreEqual( MipFeedType.Availability, feeds[ 0 ] );
            Assert.AreEqual( MipFeedType.Distribution, feeds[ 1 ] );
            Assert.AreEqual( MipFeedType.Product, feeds[ 2 ] );
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void Create_Run_SaveRestore_Proceed_Task_PublishItemOnEbay()
        {
            var dispatcher = Dispatcher.Instance;
            var store = DemoshopStoreManager.Instance;

            store.CreateTask(DemoshopStoreTaskType.PublishOnEbay);
            store.SaveData();
            store.RestoreData();

            dispatcher.RunChannelTasks(store.GetChannelTasks());
            dispatcher.ProceedChannelTasks(store.GetChannelTasks());

            store.SaveData();
            store.RestoreData();

            Assert.AreEqual(1, store.StoreTasks.Count);
            Assert.AreEqual(3, store.GetChannelTasks().Count());

            store.GetChannelTasks().OfType<EbayPublishTask>().ForEach(t =>
            {
                Trace.WriteLine(t);
                Assert.IsTrue(t.GetStatusCode() == TaskStatus.Inprocess || t.GetStatusCode() == TaskStatus.Success);
                Assert.IsNotNull(t.EbayPublishResponse.Result.MipRequestId);
            });
        }
    }
}