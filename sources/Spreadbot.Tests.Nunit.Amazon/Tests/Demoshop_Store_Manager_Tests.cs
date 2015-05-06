// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Demoshop_Store_Manager_Tests.cs

using System;
using System.Linq;
using MoreLinq;
using NUnit.Framework;
using Spreadbot.Core.Abstracts.Store.Operations.Tasks;
using Spreadbot.Core.Channels.Amazon.Operations.Tasks;
using Spreadbot.Core.Stores.Demoshop.Manager;
using Spreadbot.Core.System.Dispatcher;
using Spreadbot.Nunit.Amazon.Base;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Nunit.Amazon.Tests
{
    [TestFixture]
    public class Demoshop_Store_Manager_Tests : Amazon_Tests
    {
        // --------------------------------------------------------[]
        [SetUp]
        public void DeleteAllStoreTasks()
        {
            using( var store = new DemoshopStoreManager() ) {
                store.DeleteAllTasks();
            }
        }

        [Test]
        public void Demoshop_Initialized_And_Read_Config()
        {
            using( new DemoshopStoreManager() ) {}
        }

        [Test]
        public void Create_Task_Submit_Item_To_Amazon()
        {
            using( var store = new DemoshopStoreManager() ) {
                var task = store.CreateTask( StoreTaskType.SubmitToAmazon );

                Assert.AreEqual( TaskStatus.Todo, task.GetStatusCode() );
                Assert.IsNull( task.AbstractResponse );
                task.AbstractSubTasks.ForEach( ct => { Assert.IsTrue( ct.IsCritical ); } );
            }
        }

        [Test]
        [Ignore( "Waiting for Mws_Tests passed" )]
        public void Run_Task_Submit_Item_To_Amazon()
        {
            using( var store = new DemoshopStoreManager() ) {
                var task = store.CreateTask( StoreTaskType.SubmitToAmazon );
                Dispatcher.Instance.RunChannelTasks( store.GetChannelTasks() );

                task.AbstractSubTasks.OfType< AmazonSubmissionTask >().ForEach( t => {
                    //IgnoreMipQueueDepthErrorMessage( t.AmazonSubmissionResponse );
                    Console.WriteLine( t.AmazonSubmissionResponse );
                    Assert.AreEqual( TaskStatus.InProgress, t.GetStatusCode() );
                    Assert.IsNotNull( t.AmazonSubmissionResponse.Result.MwsRequestId );
                } );
                Assert.AreEqual( TaskStatus.InProgress, task.GetStatusCode() );
            }
        }

        [Test]
        public void Proceed_Task_Submit_Item_To_Amazon() {}
    }
}