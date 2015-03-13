using System.Collections.Generic;

namespace Spreadbot.Sdk.Common
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