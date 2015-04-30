// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.pvt.Client.cs

using MarketplaceWebService;
using Spreadbot.Core.Channels.Amazon.Configuration.Settings;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Connector
{
    public partial class MwsConnector
    {
        private MarketplaceWebServiceClient _mwsClient;

        private void InitServiceClient()
        {
            var mwsConfig = new MarketplaceWebServiceConfig();
            mwsConfig.SetUserAgentHeader( "Speadbot", "1.0", "C#" );
            mwsConfig.ServiceURL = AmazonSettings.ServiceUrl;

            _mwsClient = new MarketplaceWebServiceClient(
                AmazonSettings.AwsAccessKeyId,
                AmazonSettings.AwsSecretAccessKey,
                mwsConfig );
        }
    }
}