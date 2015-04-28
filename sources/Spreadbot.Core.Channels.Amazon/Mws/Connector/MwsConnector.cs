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

        public static MwsConnector Instance
        {
            get { return _instance ?? ( _instance = new MwsConnector() ); }
        }

        // --------------------------------------------------------[]
        public virtual MwsResponse< MwsSubmitFeedResult > SubmitFeed( MwsFeedHandler mwsFeedHandler )
        {
            return SubmitFeed( mwsFeedHandler, MwsRequestHandler.GenerateId() );
        }

        // --------------------------------------------------------[]
        public MwsResponse< MwsSubmitFeedResult > SubmitFeed( MwsFeedHandler mwsFeedHandler, string reqId )
        {
            return _SubmitFeed( mwsFeedHandler, reqId );
        }
    }
}