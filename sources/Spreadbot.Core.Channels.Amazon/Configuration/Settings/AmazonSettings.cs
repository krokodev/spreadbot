// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// AmazonSettings.cs

using Krokodev.Common.Extensions;
using Spreadbot.Core.Channels.Amazon.Configuration.Sections;

namespace Spreadbot.Core.Channels.Amazon.Configuration.Settings
{
    public static class AmazonSettings
    {
        public static string ServiceUrl
        {
            get { return AmazonPublicConfig.Instance.MwsConnection.ServiceUrl; }
        }

        public static string MarketplaceId
        {
            get { return AmazonPublicConfig.Instance.MwsConnection.MarketplaceId; }
        }

        public static string AwsAccessKeyId
        {
            get { return AmazonSecretConfig.Instance.MwsSecretData.AwsAccessKeyId; }
        }

        public static string AwsSecretAccessKey
        {
            get { return AmazonSecretConfig.Instance.MwsSecretData.AwsSecretAccessKey; }
        }

        public static string MerchantId
        {
            get { return AmazonSecretConfig.Instance.MwsSecretData.MerchantId; }
        }

        public static string XmlMerchantIdentifier
        {
            get { return AmazonSecretConfig.Instance.MwsSecretData.XmlMerchantIdentifier; }
        }
        

        public static string FeedsPaths
        {
            get { return MapToDataDirectory( AmazonPublicConfig.Instance.MwsPaths.FeedsPath ); }
        }

        public static string BasePath
        {
            get { return MapToDataDirectory( AmazonPublicConfig.Instance.MwsPaths.BasePath ); }
        }

        private static string MapToDataDirectory( string path )
        {
            return path.MapPathToDataDirectory();
        }

        
    }
}