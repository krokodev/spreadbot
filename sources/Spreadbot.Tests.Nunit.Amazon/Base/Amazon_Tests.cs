// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Amazon_Tests.cs

using Spreadbot.Core.Channels.Amazon.Services.Mws.Connector;
using Spreadbot.Nunit.Base;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Nunit.Amazon.Base
{
    public class Amazon_Tests : Spreadbot_Tests
    {
        protected static void Ignore_Mws_Throttling( IAbstractResponse response )
        {
            Ignore_Mws_Throttling( response, response.GetType().ToString() );
        }

        protected static void Ignore_Mws_Throttling( object obj, string comment )
        {
            Assert_Inconclusive_if_Text_Contains_Message( obj, MwsConnector.MwsRequestIsThrottled, comment );
            Assert_Inconclusive_if_Text_Contains_Message( obj, MwsConnector.MwsYouExceededYourQuota, comment );
        }

        protected static void Ignore_Some_Errors_Advisely_Generated_by_Tests( object text )
        {
            const string cause = "Cause some tests may generate such errors advisedly";
            Assert_Inconclusive_if_Text_Contains_Message( text, "<ResultDescription>Error validating XML document", cause );
            Assert_Inconclusive_if_Text_Contains_Message( text, "Please specify the correct feed type when re-submitting this feed", cause );
        }
    }
}