// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Demoshop_StoreManager_Tests.cs

using NUnit.Framework;
using Spreadbot.Core.Stores.Demoshop.Manager;
using Spreadbot.Nunit.Amazon.Base;

namespace Spreadbot.Nunit.Amazon.Tests
{
    [TestFixture]
    public class Demoshop_StoreManager_Tests : Amazom_Tests
    {
        // --------------------------------------------------------[]
        [SetUp]
        public void DeleteAllStoreTasks()
        {
/*
            using( var store = new DemoshopStoreManager() ) {
                store.DeleteAllTasks();
            }
*/
        }

        // --------------------------------------------------------[]
        [Test]
        public void Create_Task_Submit_Item_To_Amazon()
        {
            /*
            var store = new DemoshopStoreManager();
            var task = store.CreateTask( StoreTaskType.SubmitToAmazon );

            Assert.AreEqual( TaskStatus.Todo, task.GetStatusCode() );
            Assert.IsNull( task.AbstractResponse );
            task.AbstractSubTasks.ForEach( ct => { Assert.IsTrue( ct.IsCritical ); } );
*/
        }

        [Test]
        public void Run_Task_Submit_Item_To_Amazon() {}

        [Test]
        public void Proceed_Task_Submit_Item_To_Amazon() {}
    }
}