// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// DemoshopStoreManager_Tests.cs
// romak_000, 2015-03-20 16:07

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadbot.Core.Stores.Demoshop.Manager;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Tests.Core.Stores.Demoshop.Manager
{
    [TestClass]
    public class DemoshopStoreManager_Tests
    {
        // --------------------------------------------------------[]
        [ClassInitialize()]
        public static void Init( TestContext testContext ) {}

        // --------------------------------------------------------[]
        [TestMethod]
        public void Create_Task_PublishItemOnEbay()
        {
            var store = DemoshopStoreManager.Instance;
            var task = store.PublishItemOnEbay();

            Assert.AreEqual(TaskStatus.Todo, task.GetStatusCode());          
        }
    }
}