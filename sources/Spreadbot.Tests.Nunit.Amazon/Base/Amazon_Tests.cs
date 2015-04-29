// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Amazon_Tests.cs

using Spreadbot.Core.Channels.Amazon.Mws.Connector;
using Spreadbot.Nunit.Base;

namespace Spreadbot.Nunit.Amazon.Base
{
    public class Amazon_Tests : Spreadbot_Tests {
        protected static void IgnoreMwsThrottling( object obj )
        {
            Assert_Inconclusive_if_Text_Contains_Message( obj.ToString(), MwsConnector.MwsRequestIsThrottled );
        }
    }
}