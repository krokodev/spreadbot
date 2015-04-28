// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// AmazonChannelManager.imp.IChannelManager.cs

using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;

namespace Spreadbot.Core.Channels.Amazon.Manager
{
    public partial class AmazonChannelManager
    {
        // --------------------------------------------------------[]
        public override void RunSubmissionTask( IChannelTask task )
        {
            //DoRunSubmissionTask( ( AmazonSubmissionTask ) task );
        }

        // --------------------------------------------------------[]
        public override void ProceedTask( IChannelTask channelTask )
        {
            //DoProceedTask( ( AmazonSubmissionTask ) channelTask );
        }

        // --------------------------------------------------------[]
        public override string Id
        {
            get { return ConstId; }
        }
    }
}