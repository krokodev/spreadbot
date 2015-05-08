// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.pvt.Client.cs

using MarketplaceWebService;
using MarketplaceWebServiceProducts;
using Spreadbot.Core.Channels.Amazon.Configuration.Settings;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Connector
{
    public partial class MwsConnector
    {
        private MarketplaceWebServiceClient _mwsFeedClient;
        private MarketplaceWebServiceProductsClient _mswProductsClient;

        private void InitMwsClients()
        {
            InitMwsFeedClient();
            InitMwsProductClient();
        }

        private void InitMwsFeedClient()
        {
            var mwsConfig = new MarketplaceWebServiceConfig();
            mwsConfig.SetUserAgentHeader( "Speadbot", "1.0", "C#" );
            mwsConfig.ServiceURL = AmazonSettings.ServiceUrl;

            _mwsFeedClient = new MarketplaceWebServiceClient(
                AmazonSettings.AwsAccessKeyId,
                AmazonSettings.AwsSecretAccessKey,
                mwsConfig );
        }
        private void InitMwsProductClient()
        {
            var mwsConfig = new MarketplaceWebServiceProductsConfig();
            mwsConfig.SetUserAgentHeader( "Speadbot", "1.0", "C#" );
            mwsConfig.ServiceURL = AmazonSettings.ServiceUrl;

            _mswProductsClient = new MarketplaceWebServiceProductsClient(
                AmazonSettings.AwsAccessKeyId,
                AmazonSettings.AwsSecretAccessKey,
                mwsConfig );
        }
    }
}