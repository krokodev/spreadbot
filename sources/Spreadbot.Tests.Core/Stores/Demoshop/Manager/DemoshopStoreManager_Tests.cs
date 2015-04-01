// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// DemoshopStoreManager_Tests.cs
// Roman, 2015-04-01 4:59 PM

using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;
using Spreadbot.Core.Stores.Demoshop.Manager;
using Spreadbot.Core.Stores.Demoshop.Operations.Tasks;
using Spreadbot.Core.System.Dispatcher;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Tasks;
using Spreadbot.Tests.Core.Common;

//using System.Diagnostics;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Tests.Core.Stores.Demoshop.Manager
{
    [TestFixture]
    public class DemoshopStoreManager_Tests : SpreadbotBaseTest
    {
        // --------------------------------------------------------[]
        [SetUp]
        public void DeleteAllStoreTasks()
        {
            DemoshopStoreManager.Instance.DeleteAllTasks();
        }

        // --------------------------------------------------------[]
        private static IEnumerable< EbayPublishTask > DemoshopEbayPublishTasks()
        {
            return DemoshopStoreManager.Instance.GetChannelTasks().OfType< EbayPublishTask >();
        }

        // --------------------------------------------------------[]
        [Test]
        public void Create_Task_PublishItemOnEbay()
        {
            var store = DemoshopStoreManager.Instance;
            var task = store.CreateTask( DemoshopStoreTaskType.PublishOnEbay );

            Assert.AreEqual( TaskStatus.Todo, task.GetStatusCode() );
            Assert.IsNull( task.AbstractResponse );
            task.AbstractSubTasks.ForEach( ct => { Assert.IsTrue( ct.IsCritical ); } );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Run_Task_PublishItemOnEbay()
        {
            var store = DemoshopStoreManager.Instance;
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
                var store = DemoshopStoreManager.Instance;
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
            // Code: Test: Save_and_Restore_Tasks()

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
        [Test]
        public void Create_Run_SaveRestore_Proceed_Task_PublishItemOnEbay()
        {
            try {
                var dispatcher = Dispatcher.Instance;
                var store = DemoshopStoreManager.Instance;

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
                    Console.WriteLine( t );
                    Assert.IsTrue( t.GetStatusCode() == TaskStatus.Inprocess || t.GetStatusCode() == TaskStatus.Success );
                    Assert.IsNotNull( t.EbayPublishResponse.Result.MipRequestId );
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
                var store = DemoshopStoreManager.Instance;

                store.CreateTask( DemoshopStoreTaskType.PublishOnEbay );
                AssertThatLastUpdateTimeIsCorrect();

                dispatcher.RunChannelTasks( store.GetChannelTasks() );
                AssertThatLastUpdateTimeIsCorrect();

                dispatcher.ProceedChannelTasks( store.GetChannelTasks() );
                AssertThatLastUpdateTimeIsCorrect();
            }
            catch( SpreadbotException exception ) {
                IgnoreMipQueueDepthErrorMessage( exception.Message );
            }
        }

        // --------------------------------------------------------[]
        private static void AssertThatLastUpdateTimeIsCorrect()
        {
            DemoshopEbayPublishTasks()
                .ForEach( t => { Assert.That( t.LastUpdateTime, Is.EqualTo( DateTime.Now ).Within( 30 ).Seconds ); } );
        }
    }
}