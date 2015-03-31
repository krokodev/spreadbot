﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// IStoreManager.cs
// Roman, 2015-03-31 1:26 PM

using System.Collections.Generic;
using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;
using Spreadbot.Core.Abstracts.Store.Operations.Tasks;

namespace Spreadbot.Core.Abstracts.Store.Manager
{
    public interface IStoreManager
    {
        IEnumerable< IChannelTask > GetChannelTasks();
        IEnumerable< IStoreTask > StoreTasks { get; }
        string Id { get; }
    }
}