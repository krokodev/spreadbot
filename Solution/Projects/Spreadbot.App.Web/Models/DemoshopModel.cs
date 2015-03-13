// !>> Model | DemoshopModel

using System.Collections.Generic;
using System.Linq;
using Spreadbot.Core.System;
using Spreadbot.Sdk.Common;

namespace Spreadbot.App.Web
{
    public class DemoshopModel
    {
        public DemoshopItem Item { get; set; }

        public static IEnumerable<IStoreTask> StoreTasks
        {
            get { return Store.StoreTasks; }           
        }
        public IEnumerable<IChannelTask> ChannelTasksTodo
        {
            get { return Store.ChannelTasks.Where(t=>t.StatusCode==TaskStatus.Todo); }
        }

        public IEnumerable<IChannelTask> ChannelTasksInprocess
        {
            get { return Store.ChannelTasks.Where(t => t.StatusCode == TaskStatus.Inprocess); }
        }

        public static IEnumerable<IChannelTask> ChannelTasks
        {
            get { return Store.ChannelTasks; }
        }

        public static DemoshopStore Store
        {
            get { return DemoshopStore.Instance; }
        }

        public static void SaveItem(DemoshopItem item)
        {
            Store.SaveItem(item);
        }

        public static void PublishItemOnEbay()
        {
            Store.PublishItemOnEbay();
        }
    }
}