// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayChannelManager.pvt.Feed.cs
// romak_000, 2015-03-20 13:56

using System.IO;
using MoreLinq;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;

namespace Spreadbot.Core.Channels.Ebay.Manager
{
    public partial class EbayChannelManager
    {
        // --------------------------------------------------------[]
        private static void CreateFeedFile( MipFeedHandler mipFeedHandler )
        {
            CreateFeedFolderIfNeed( mipFeedHandler );

            var fileName = MipConnector.LocalFeedXmlFilePath( mipFeedHandler );
            using( var file = File.CreateText( fileName ) ) {
                file.Write( mipFeedHandler.Content );
            }
        }

        // --------------------------------------------------------[]
        private static void CreateFeedFolderIfNeed( MipFeedHandler mipFeedHandler )
        {
            var feedFolder = MipConnector.LocalFeedFolder( mipFeedHandler );
            if( !Directory.Exists( feedFolder ) ) {
                Directory.CreateDirectory( feedFolder );
            }
        }

        // --------------------------------------------------------[]
        private static void EraseFeedFolder( MipFeedHandler mipFeedHandler )
        {
            Directory
                .GetFiles( MipConnector.LocalFeedFolder( mipFeedHandler ) )
                .ForEach( File.Delete );
        }
    }
}