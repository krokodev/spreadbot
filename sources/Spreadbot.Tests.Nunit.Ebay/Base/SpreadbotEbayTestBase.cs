// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Ebay
// SpreadbotEbayTestBase.cs

using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Nunit.Base;

namespace Spreadbot.Nunit.Ebay.Base
{
    public class SpreadbotEbayTestBase : SpreadbotTestBase
    {
        protected static void IgnoreMipQueueDepthErrorMessage( object obj )
        {
            Assert_Inconclusive_if_Text_Contains_Message( obj.ToString(), MipConnector.MipQueueDepthErrorMessage );
        }
    }
}