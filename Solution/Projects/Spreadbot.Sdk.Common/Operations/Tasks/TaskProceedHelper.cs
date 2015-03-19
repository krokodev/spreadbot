// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// TaskProceedHelper.cs
// romak_000, 2015-03-19 15:49

using System.Collections.Generic;

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public class TaskProceedHelper
    {
        private readonly List<ITaskProceedInfo> _proceedHistory = new List<ITaskProceedInfo>();

        public void Save(ITaskProceedInfo info)
        {
            _proceedHistory.Add(info);
        }
    }
}