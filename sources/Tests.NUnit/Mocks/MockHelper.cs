// Spreadbot (c) 2015 Crocodev
// Tests.NUnit
// MockHelper.cs
// Roman, 2015-04-07 12:15 PM

using System;
using System.IO;
using Moq;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Core.Channels.Ebay.Mip.SftpHelper;

namespace Tests.NUnit.Mocks
{
    internal class MockHelper
    {
        // Code: MockHelper, style
        public static IMipConnector GetMipConnectorForUsingLocalData()
        {
            var mockMipConnector = new Mock< MipConnector > { CallBase = true };
            var mockSftpHelper = new Mock< WinScpSftpHelper > { CallBase = true };

            ConfigureSftpHelperToGetContentFromLocalFolder( mockSftpHelper );

            mockMipConnector.Object.SftpHelper = mockSftpHelper.Object;
            return mockMipConnector.Object;
        }

        // --------------------------------------------------------[]
        private static void ConfigureSftpHelperToGetContentFromLocalFolder( Mock< WinScpSftpHelper > mockSftpHelper )
        {
            mockSftpHelper.Setup( helper => helper.FindRemoteFile(
                It.IsAny< string >(),
                It.Is< String >( s => s.ToLower().Contains( "inproc" ) ) ) )
                .Returns( new MipResponse< MipFindRemoteFileResult > {
                    IsSuccess = false,
                    StatusCode = MipOperationStatus.FindRemoteFileFailure
                } );

            mockSftpHelper.Setup( helper => helper.FindRemoteFile(
                It.IsAny< string >(),
                It.Is< String >( s => s.ToLower().Contains( "output" ) ) ) )
                .Returns( (
                    string filePrefix,
                    string remoteDir )
                    => new MipResponse< MipFindRemoteFileResult > {
                        IsSuccess = true,
                        StatusCode = MipOperationStatus.FindRemoteFileSuccess,
                        Result = new MipFindRemoteFileResult {
                            RemoteDir = "fake",
                            RemoteFileName = filePrefix + ".xml",
                        }
                    } );

            mockSftpHelper.Setup( helper => helper.GetRemoteFileContent(
                It.IsAny< string >(),
                It.IsAny< string >(),
                It.IsAny< string >() ) )
                .Returns( ( string remoteFolder, string fileName, string localFolder ) => {
                    var filePath = string.Format( @"{0}\{1}", localFolder, fileName );
                    return File.ReadAllText( filePath );
                } );
        }
    }
}