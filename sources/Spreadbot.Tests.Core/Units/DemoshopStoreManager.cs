// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// DemoshopStoreManager.cs
// Roman, 2015-04-03 1:45 PM

using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;
using Spreadbot.Core.Stores.Demoshop.Operations.Tasks;
using Spreadbot.Core.System.Dispatcher;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Tasks;
using Spreadbot.Tests.Core.Code;

//using System.Diagnostics;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Tests.Core.Units
{
    [TestFixture]
    public class DemoshopStoreManager : SpreadbotTestBase
    {
        // --------------------------------------------------------[]
        [SetUp]
        public void DeleteAllStoreTasks()
        {
            Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance.DeleteAllTasks();
        }

        // --------------------------------------------------------[]
        private static IEnumerable< EbayPublishTask > DemoshopEbayPublishTasks()
        {
            return
                Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance.GetChannelTasks()
                    .OfType< EbayPublishTask >();
        }

        // --------------------------------------------------------[]
        [Test]
        public void Create_Task_PublishItemOnEbay()
        {
            var store = Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance;
            var task = store.CreateTask( DemoshopStoreTaskType.PublishOnEbay );

            Assert.AreEqual( TaskStatus.Todo, task.GetStatusCode() );
            Assert.IsNull( task.AbstractResponse );
            task.AbstractSubTasks.ForEach( ct => { Assert.IsTrue( ct.IsCritical ); } );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Run_Task_PublishItemOnEbay()
        {
            var store = Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance;
            var task = store.CreateTask( DemoshopStoreTaskType.PublishOnEbay );
            Dispatcher.Instance.RunChannelTasks( store.GetChannelTasks() );

            task.AbstractSubTasks.OfType< EbayPublishTask >().ForEach( t => {
                IgnoreMipQueueDepthErrorMessage( t.EbayPublishResponse.ToString() );
                Console.WriteLine( t.EbayPublishResponse );
                Assert.AreEqual( TaskStatus.Inprocess, t.GetStatusCode() );
                Assert.IsNotNull( t.EbayPublishResponse.Result.MipRequestId );
            } );
            Assert.AreEqual( TaskStatus.Inprocess, task.GetStatusCode() );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Proceed_Task_PublishItemOnEbay()
        {
            try {
                var dispatcher = Dispatcher.Instance;
                var store = Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance;
                var task = store.CreateTask( DemoshopStoreTaskType.PublishOnEbay );

                dispatcher.RunChannelTasks( store.GetChannelTasks() );
                dispatcher.ProceedChannelTasks( store.GetChannelTasks() );

                task.AbstractSubTasks.OfType< EbayPublishTask >().ForEach( t => {
                    IgnoreMipQueueDepthErrorMessage( t.EbayPublishResponse.ToString() );
                    Console.WriteLine( t );
                    Assert.IsTrue( t.GetStatusCode() == TaskStatus.Inprocess || t.GetStatusCode() == TaskStatus.Success );
                    Assert.IsNotNull( t.EbayPublishResponse.Result.MipRequestId );
                } );
                Assert.AreEqual( TaskStatus.Inprocess, task.GetStatusCode() );
            }
            catch( SpreadbotException exception ) {
                IgnoreMipQueueDepthErrorMessage( exception.Message );
            }
        }

        // --------------------------------------------------------[]
        [Test]
        public void Save_and_Restore_Tasks()
        {
            // StatusCode: Test: Save_and_Restore_Tasks()

            var store = Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance;

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
        [Test]
        public void Create_Run_SaveRestore_Proceed_Task_PublishItemOnEbay()
        {
            try {
                var dispatcher = Dispatcher.Instance;
                var store = Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance;

                store.CreateTask( DemoshopStoreTaskType.PublishOnEbay );
                store.SaveData();
                store.RestoreData();

                dispatcher.RunChannelTasks( store.GetChannelTasks() );
                dispatcher.ProceedChannelTasks( store.GetChannelTasks() );

                store.SaveData();
                store.RestoreData();

                Assert.AreEqual( 1, store.StoreTasks.Count );
                Assert.AreEqual( 3, store.GetChannelTasks().Count() );

                DemoshopEbayPublishTasks().ForEach( t => {
                    Console.WriteLine();
                    Console.WriteLine( t );
                    Assert.IsTrue( t.GetStatusCode() == TaskStatus.Inprocess || t.GetStatusCode() == TaskStatus.Success );
                    Assert.IsNotNull( t.EbayPublishResponse.Result.MipRequestId );
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
                var store = Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance;

                store.CreateTask( DemoshopStoreTaskType.PublishOnEbay );
                Assert_That_Last_Update_Time_is_Correct();

                dispatcher.RunChannelTasks( store.GetChannelTasks() );
                Assert_That_Last_Update_Time_is_Correct();

                dispatcher.ProceedChannelTasks( store.GetChannelTasks() );
                Assert_That_Last_Update_Time_is_Correct();
            }
            catch( SpreadbotException exception ) {
                IgnoreMipQueueDepthErrorMessage( exception.Message );
            }
        }

        // --------------------------------------------------------[]
        [Test]
        public void Run_Wrong_Task_PublishItemOnEbay_Contains_Trace_Info()
        {
            var store = Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance;
            var task = store.Mock_CreateTask( DemoshopStoreTaskType.PublishOnEbay );
            Dispatcher.Instance.RunChannelTasks( store.GetChannelTasks() );

            Console.WriteLine( task );

            Assert.That( task.GetStatusCode() == TaskStatus.Failure, "Task failure" );
            Assert_That_Text_Contains( task, MipConnector.MipWriteToLocationErrorMessage );
        }

        [Test]
        public void Task_Num_Is_The_Same_After_Reload_Without_Deleting()
        {
            var store = Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance;
            store.Mock_CreateTask( DemoshopStoreTaskType.PublishOnEbay );
            var taskNum = store.GetChannelTasks().Count();

            Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance.SaveData();
            Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance.RestoreData();

            var task = Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance.StoreTasks.First();
            Assert.AreEqual( taskNum, task.AbstractSubTasks.Count(), "Task num" );
        }

        [Test]
        public void Task_Keeps_Id_After_Reload()
        {
            var store = Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance;
            var id = store.Mock_CreateTask( DemoshopStoreTaskType.PublishOnEbay ).Id;

            Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance.SaveData();
            Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance.RestoreData();

            var task = Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance.StoreTasks.First();
            Assert.AreEqual( id, task.Id, "Task.Id" );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Run_Wrong_Task_PublishItemOnEbay_Contains_Trace_Info_After_Reload()
        {
            var store = Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance;
            store.Mock_CreateTask( DemoshopStoreTaskType.PublishOnEbay );
            Dispatcher.Instance.RunChannelTasks( store.GetChannelTasks() );

            Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance.SaveData();
            Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance.DeleteAllTasks();
            Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance.RestoreData();
            var task = Spreadbot.Core.Stores.Demoshop.Manager.DemoshopStoreManager.Instance.StoreTasks.First();

            Console.WriteLine( task );

            Assert.That( task.GetStatusCode() == TaskStatus.Failure, "Task failure" );
            Assert_That_Text_Contains( task, MipConnector.MipWriteToLocationErrorMessage );
        }

        // --------------------------------------------------------[]
        private static void Assert_That_Last_Update_Time_is_Correct()
        {
            DemoshopEbayPublishTasks()
                .ForEach( t => { Assert.That( t.LastUpdateTime, Is.EqualTo( DateTime.Now ).Within( 30 ).Seconds ); } );
        }
    }
}