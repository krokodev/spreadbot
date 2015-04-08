// Spreadbot (c) 2015 Crocodev
// Tests.NUnit
// DemoshopController_Tests.cs
// Roman, 2015-04-08 1:21 PM

using System;
using System.Collections.Generic;
using System.Linq;
using Crocodev.Common.Extensions;
using NUnit.Framework;
using Spreadbot.App.Web.Models;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Tests.NUnit.Units
{
    [TestFixture]
    public class DemoshopController_Tests
    {
        [Test]
        public void IndexTest()
        {
            var message = "";
            using( var m = new DemoshopModel() ) {
                m.DeleteTasks();
                m.CreateTaskPublishItemOnEbay();
                m.Message = ( message = "Task [{0}] added".SafeFormat( m.StoreTasks.First().Id ) );
            }
            var model = new DemoshopModel();

            Console.WriteLine(message);

            Assert.AreEqual( 1, model.StoreTasks.Count(), "StoreTasks.Count" );
            Assert.AreEqual( message, model.Message, "Message" );

            ShowTasks( model.StoreTasks );
        }

        private void ShowTasks( IEnumerable< IAbstractTask > tasks )
        {
            var enumerable = tasks.ToList();
            if( enumerable.Count() != 0 ) {
                foreach( var task in enumerable ) {
                    var qq1 = task.GetStatusCode();

                    task.GetBriefInfo();
                    var qq2 = task.Id;

                    var qq3 = task as EbayPublishTask;

                    ShowTasks( task.AbstractSubTasks );
                }
            }
        }
    }
}