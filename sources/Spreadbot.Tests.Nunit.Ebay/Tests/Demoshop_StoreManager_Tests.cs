// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Ebay
// Demoshop_StoreManager_Tests.cs

using System;
using System.Linq;
using MoreLinq;
using NUnit.Framework;
using Spreadbot.Core.Abstracts.Store.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Stores.Demoshop.Manager;
using Spreadbot.Core.System.Dispatcher;
using Spreadbot.Nunit.Ebay.Base;
using Spreadbot.Nunit.Ebay.Mocks;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Nunit.Ebay.Tests
{
    [TestFixture]
    public class Demoshop_StoreManager_Tests : Ebay_Tests
    {
        // --------------------------------------------------------[]
        [SetUp]
        public void DeleteAllStoreTasks()
        {
            using( var store = new DemoshopStoreManager() ) {
                store.DeleteAllTasks();
            }
        }

        // --------------------------------------------------------[]
        [Test]
        public void Create_Task_Submit_Item_To_Ebay()
        {
            var store = new DemoshopStoreManager();
            var task = store.CreateTask( StoreTaskType.SubmitToEbay );

            Assert.AreEqual( TaskStatus.Todo, task.GetStatusCode() );
            Assert.IsNull( task.AbstractResponse );
            task.AbstractSubTasks.ForEach( ct => { Assert.IsTrue( ct.IsCritical ); } );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Run_Task_Submit_Item_To_Ebay()
        {
            var store = new DemoshopStoreManager();
            var task = store.CreateTask( StoreTaskType.SubmitToEbay );
            Dispatcher.Instance.RunChannelTasks( store.GetChannelTasks() );

            task.AbstractSubTasks.OfType< EbaySubmissionTask >().ForEach( t => {
                IgnoreMipQueueDepthErrorMessage( t.EbaySubmissionResponse );
                Console.WriteLine( t.EbaySubmissionResponse );
                Assert.AreEqual( TaskStatus.InProgress, t.GetStatusCode() );
                Assert.IsNotNull( t.EbaySubmissionResponse.Result.MipSubmissionId );
            } );
            Assert.AreEqual( TaskStatus.InProgress, task.GetStatusCode() );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Proceed_Task_Submit_Item_To_Ebay()
        {
            try {
                var dispatcher = Dispatcher.Instance;
                var store = new DemoshopStoreManager();
                var task = store.CreateTask( StoreTaskType.SubmitToEbay );

                dispatcher.RunChannelTasks( store.GetChannelTasks() );
                dispatcher.ProceedChannelTasks( store.GetChannelTasks() );

                task.AbstractSubTasks.OfType< EbaySubmissionTask >().ForEach( t => {
                    IgnoreMipQueueDepthErrorMessage( t.EbaySubmissionResponse );
                    Console.WriteLine( t );
                    Assert.IsTrue( t.GetStatusCode() == TaskStatus.InProgress || t.GetStatusCode() == TaskStatus.Success );
                    Assert.IsNotNull( t.EbaySubmissionResponse.Result.MipSubmissionId );
                } );
                Assert.AreEqual( TaskStatus.InProgress, task.GetStatusCode() );
            }
            catch( SpreadbotException exception ) {
                IgnoreMipQueueDepthErrorMessage( exception.Message );
            }
        }

        // --------------------------------------------------------[]
        [Test]
        public void Save_and_Restore_Tasks()
        {
            var store = new DemoshopStoreManager();

            store.CreateTask( StoreTaskType.SubmitToEbay );
            store.SaveData();
            store.DeleteAllTasks();
            store.LoadData();

            Assert.AreEqual( 1, store.StoreTasks.Count );
            Assert.AreEqual( 3, store.GetChannelTasks().Count() );

            var feeds =
                store.GetChannelTasks()
                    .OfType< EbaySubmissionTask >()
                    .Select( t => t.Args.MipFeedDescriptor.Type )
                    .OrderBy( f => f.ToString() ).ToArray();

            Assert.AreEqual( 3, feeds.Count() );
            Assert.AreEqual( MipFeedType.Availability, feeds[ 0 ] );
            Assert.AreEqual( MipFeedType.Distribution, feeds[ 1 ] );
            Assert.AreEqual( MipFeedType.Product, feeds[ 2 ] );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Create_Run_SaveRestore_Proceed_Task_Submit_Item_To_Ebay()
        {
            try {
                var dispatcher = Dispatcher.Instance;
                var store = new DemoshopStoreManager();

                store.CreateTask( StoreTaskType.SubmitToEbay );
                store.SaveData();
                store.LoadData();

                dispatcher.RunChannelTasks( store.GetChannelTasks() );
                dispatcher.ProceedChannelTasks( store.GetChannelTasks() );

                store.SaveData();
                store.LoadData();

                Assert.AreEqual( 1, store.StoreTasks.Count );
                Assert.AreEqual( 3, store.GetChannelTasks().Count() );

                store.GetEbaySubmissionTasks().ForEach( t => {
                    Console.WriteLine();
                    Console.WriteLine( t );
                    Assert.IsTrue(
                        t.GetStatusCode() == TaskStatus.InProgress || t.GetStatusCode() == TaskStatus.Success,
                        "Success or InProgress" );
                    Assert.IsNotNull( t.EbaySubmissionResponse.Result.MipSubmissionId );
                    Assert_That_Text_Contains( t, "ArgsInfo" );
                } );
            }
            catch( SpreadbotException exception ) {
                IgnoreMipQueueDepthErrorMessage( exception.Message );
            }
        }

        // --------------------------------------------------------[]
        [Test]
        public void ChannelTasks_LastUpdateTime()
        {
            try {
                var dispatcher = Dispatcher.Instance;
                var store = new DemoshopStoreManager();

                store.CreateTask( StoreTaskType.SubmitToEbay );
                Assert_That_Last_Update_Time_is_Correct( store );

                dispatcher.RunChannelTasks( store.GetChannelTasks() );
                Assert_That_Last_Update_Time_is_Correct( store );

                dispatcher.ProceedChannelTasks( store.GetChannelTasks() );
                Assert_That_Last_Update_Time_is_Correct( store );
            }
            catch( SpreadbotException exception ) {
                IgnoreMipQueueDepthErrorMessage( exception.Message );
            }
        }

        // --------------------------------------------------------[]
        [Test]
        public void Run_Wrong_Task_SubmitItemToEbay_Contains_Trace_Info()
        {
            var store = EbayMockHelper.GetDemoshopStoreManagerCreatingSimpleSubmitToEbayTask();

            var task = store.CreateTask( StoreTaskType.SubmitToEbay );
            Dispatcher.Instance.RunChannelTasks( store.GetChannelTasks() );

            Console.WriteLine( task );

            Assert.That( task.GetStatusCode() == TaskStatus.Failure, "Task failure" );
            Assert_That_Text_Contains( task, MipConnector.MipWriteToLocationErrorMessage );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Task_Num_Is_The_Same_After_Reload_Without_Deleting()
        {
            var store = new DemoshopStoreManager();
            store.CreateTask( StoreTaskType.SubmitToEbay );
            var taskNum = store.GetChannelTasks().Count();

            store.SaveData();
            store.LoadData();

            Assert.AreEqual( 1, store.StoreTasks.Count(), "Store Task Num" );
            var task = store.StoreTasks.First();
            Assert.AreEqual( taskNum, task.AbstractSubTasks.Count(), "Sub Tasks num" );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Task_Num_Is_The_Same_After_Reload_With_Deleting()
        {
            var store = new DemoshopStoreManager();
            store.CreateTask( StoreTaskType.SubmitToEbay );
            var taskNum = store.GetChannelTasks().Count();

            store.SaveData();
            store.DeleteAllTasks();
            store.LoadData();

            Assert.AreEqual( 1, store.StoreTasks.Count(), "Store Task Num" );
            var task = store.StoreTasks.First();
            Assert.AreEqual( taskNum, task.AbstractSubTasks.Count(), "Sub Tasks num" );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Task_Keeps_Id_After_Reload()
        {
            var store = new DemoshopStoreManager();
            var id = store.CreateTask( StoreTaskType.SubmitToEbay ).Id;

            store.SaveData();
            store.LoadData();

            var task = store.StoreTasks.First();
            Assert.AreEqual( id, task.Id, "Task.Id" );
        }

        // ===================================================================================== []
        // Private
        private static void Assert_That_Last_Update_Time_is_Correct( DemoshopStoreManager store )
        {
            store.GetEbaySubmissionTasks()
                .ForEach( t => { Assert.That( t.LastUpdateTime, Is.EqualTo( DateTime.Now ).Within( 5 ).Minutes ); } );
        }
    }
}