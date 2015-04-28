// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Ebay
// Ebay_Tests.cs

using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Nunit.Base;

namespace Spreadbot.Nunit.Ebay.Base
{
    public class Ebay_Tests : Spreadbot_Tests
    {
        protected static void IgnoreMipQueueDepthErrorMessage( object obj )
        {
            Assert_Inconclusive_if_Text_Contains_Message( obj.ToString(), MipConnector.MipQueueDepthErrorMessage );
        }
    }
}