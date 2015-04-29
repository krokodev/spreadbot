// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.cs

using Spreadbot.Core.Channels.Amazon.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Mws.Operations.Request;
using Spreadbot.Core.Channels.Amazon.Mws.Operations.Response;
using Spreadbot.Core.Channels.Amazon.Mws.Results;

namespace Spreadbot.Core.Channels.Amazon.Mws.Connector
{
    public partial class MwsConnector : IMwsConnector
    {
        // --------------------------------------------------------[]
        private static MwsConnector _instance;
        private static readonly object Locker = new object();

        public MwsConnector()
        {
            InitServiceClient();
        }

        public static MwsConnector Instance
        {
            get
            {
                lock( Locker ) {
                    return _instance ?? ( _instance = new MwsConnector() );
                }
            }
        }

        // --------------------------------------------------------[]
        public virtual MwsResponse< MwsSubmitFeedResult > SubmitFeed( MwsFeedHandler mwsFeedHandler )
        {
            return _SubmitFeed( mwsFeedHandler, MwsRequestHandler.GenerateId() );
        }
    }
}