using System;
using Spreadbot.Core.System;

namespace Spreadbot.Core.Channel.Ebay
{
    public class EbayChannel : IChannel
    {
        private static readonly Guid Guid = new Guid("F754E71E-652A-47B0-A1BC-8D74922D25DC");
        public Guid Id
        {
            get
            {
                return Guid;
            }
        }
    }
}