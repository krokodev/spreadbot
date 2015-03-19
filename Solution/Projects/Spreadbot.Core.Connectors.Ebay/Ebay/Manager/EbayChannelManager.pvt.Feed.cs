// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayChannelManager.pvt.Feed.cs
// romak_000, 2015-03-19 20:00

using System.IO;
using MoreLinq;
using Spreadbot.Core.Connectors.Ebay.Mip.Connector;
using Spreadbot.Core.Connectors.Ebay.Mip.Feed;


namespace Spreadbot.Core.Connectors.Ebay.Channel
{
    public partial class EbayChannelManager
    {
        // --------------------------------------------------------[]
        private static void CreateFeedFile( MipFeed mipFeed )
        {
            CreateFeedFolderIfNeed( mipFeed );

            var fileName = MipConnector.LocalFeedXmlFilePath( mipFeed );
            using( var file = File.CreateText( fileName ) ) {
                file.Write( mipFeed.Content );
            }
        }


        // --------------------------------------------------------[]
        private static void CreateFeedFolderIfNeed( MipFeed mipFeed )
        {
            var feedFolder = MipConnector.LocalFeedFolder( mipFeed );
            if( !Directory.Exists( feedFolder ) ) {
                Directory.CreateDirectory( feedFolder );
            }
        }


        // --------------------------------------------------------[]
        private static void EraseFeedFolder( MipFeed mipFeed )
        {
            Directory
                .GetFiles( MipConnector.LocalFeedFolder( mipFeed ) )
                .ForEach( File.Delete );
        }
    }
}