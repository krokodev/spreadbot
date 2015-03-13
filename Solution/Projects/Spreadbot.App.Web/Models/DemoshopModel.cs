// !>> Model | DemoshopModel

using System.Collections.Generic;
using System.Linq;
using Spreadbot.Core.Common;
using Spreadbot.Core.System;
using Spreadbot.Sdk.Common;

namespace Spreadbot.App.Web
{
    public class DemoshopModel
    {
        public static DemoshopItem Item
        {
            get { return Store.Item; }
        }

        public static IEnumerable<IStoreTask> StoreTasks
        {
            get { return Store.StoreTasks; }           
        }
        public static IEnumerable<IChannelTask> ChannelTasksTodo
        {
            get { return Store.ChannelTasks.Where(t=>t.StatusCode==TaskStatus.Todo); }
        }

        public static IEnumerable<IChannelTask> ChannelTasksInprocess
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