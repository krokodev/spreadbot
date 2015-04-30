// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// EbayAdapter.pvt.Feed.cs

using System.IO;
using MoreLinq;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;

namespace Spreadbot.Core.Channels.Ebay.Adapter
{
    public partial class EbayAdapter
    {
        // --------------------------------------------------------[]
        private static void CreateFeedFile( MipFeedDescriptor mipFeedDescriptor )
        {
            CreateFeedFolderIfNeed( mipFeedDescriptor );

            var fileName = MipConnector.LocalFeedXmlFilePath( mipFeedDescriptor );
            using( var file = File.CreateText( fileName ) ) {
                file.Write( mipFeedDescriptor.Content );
            }
        }

        // --------------------------------------------------------[]
        private static void CreateFeedFolderIfNeed( MipFeedDescriptor mipFeedDescriptor )
        {
            var feedFolder = MipConnector.LocalFeedFolder( mipFeedDescriptor );
            if( !Directory.Exists( feedFolder ) ) {
                Directory.CreateDirectory( feedFolder );
            }
        }

        // --------------------------------------------------------[]
        private static void EraseFeedFolder( MipFeedDescriptor mipFeedDescriptor )
        {
            Directory
                .GetFiles( MipConnector.LocalFeedFolder( mipFeedDescriptor ) )
                .ForEach( File.Delete );
        }
    }
}