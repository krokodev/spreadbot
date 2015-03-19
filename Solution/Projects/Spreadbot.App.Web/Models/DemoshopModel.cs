// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopModel.cs
// romak_000, 2015-03-19 15:37

using System.Collections.Generic;
using System.Linq;
using Spreadbot.App.Web.Sources.Demoshop;
using Spreadbot.Core.Common.Channel.Operations.Tasks;
using Spreadbot.Core.Common.Store;
using Spreadbot.Core.Common.Store.Operations;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.App.Web.Models
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