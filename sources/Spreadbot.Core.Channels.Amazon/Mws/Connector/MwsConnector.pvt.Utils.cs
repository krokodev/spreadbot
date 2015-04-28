// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.pvt.Utils.cs

using System;
using Spreadbot.Core.Channels.Amazon.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Mws.Operations.Response;
using Spreadbot.Core.Channels.Amazon.Mws.Operations.StatusCode;
using Spreadbot.Core.Channels.Amazon.Mws.Results;

namespace Spreadbot.Core.Channels.Amazon.Mws.Connector
{
    public partial class MwsConnector
    {
        // --------------------------------------------------------[]
        protected MwsResponse< MwsSubmitFeedResult > _SubmitFeed( MwsFeedHandler mwsFeedHandler, string reqId )
        {
            try {
                // submit to mws
            }
            catch( Exception exception ) {
                return new MwsResponse< MwsSubmitFeedResult >( exception ) {
                    StatusCode = MwsOperationStatus.SubmitFeedFailure,
                };
            }

            return new MwsResponse< MwsSubmitFeedResult > {
                StatusCode = MwsOperationStatus.SubmitFeedSuccess,
                Result = new MwsSubmitFeedResult { MwsRequestId = reqId }
            };
        }
    }
}