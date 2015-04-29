// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// EbayAdapter.imp.IChannelAdapter.cs

using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;

namespace Spreadbot.Core.Channels.Ebay.Adapter
{
    public partial class EbayAdapter
    {
        // --------------------------------------------------------[]
        public void RunSubmissionTask( IChannelTask task )
        {
            DoRunSubmissionTask( ( EbaySubmissionTask ) task );
        }

        // --------------------------------------------------------[]
        public void ProceedTask( IChannelTask channelTask )
        {
            DoProceedTask( ( EbaySubmissionTask ) channelTask );
        }

        // --------------------------------------------------------[]
        public string Id
        {
            get { return ConstId; }
        }
    }
}