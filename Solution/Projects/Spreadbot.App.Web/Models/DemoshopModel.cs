// !>> Model | DemoshopModel

using System.Collections.Generic;
using System.Linq;
using Spreadbot.Core.Common;
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
            get { return ((IStore) Store).StoreTasks; }
        }

        public static IEnumerable<IChannelTask> ChannelTasksTodo
        {
            get { return ChannelTasks.Where(t => t.GetStatusCode() == TaskStatus.Todo); }
        }

        public static IEnumerable<IChannelTask> ChannelTasksInprocess
        {
            get { return ChannelTasks.Where(t => t.GetStatusCode() == TaskStatus.Inprocess); }
        }

        public static IEnumerable<IChannelTask> ChannelTasks
        {
            get { return ((IStore) Store).GetChannelTasks(); }
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

        private static readonly object Locker = 0;

        public static void Save()
        {
            lock (Locker)
            {
                Store.Save();
            }
        }

        public static void Restore()
        {
            lock (Locker)
            {
                Store.Restore();
            }
        }
    }
}