// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// AmazonAdapter.imp.IChannelAdapter.cs

using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;
using Spreadbot.Core.Channels.Amazon.Operations.Tasks;

namespace Spreadbot.Core.Channels.Amazon.Adapter
{
    public partial class AmazonAdapter
    {
        // --------------------------------------------------------[]
        public void RunSubmissionTask( IChannelTask task )
        {
            DoRunSubmissionTask( ( AmazonSubmissionTask ) task );
        }

        // --------------------------------------------------------[]
        public void ProceedTask( IChannelTask channelTask )
        {
            //DoProceedTask( ( AmazonSubmissionTask ) channelTask );
        }

        // --------------------------------------------------------[]
        public string Id
        {
            get { return ConstId; }
        }
    }
}