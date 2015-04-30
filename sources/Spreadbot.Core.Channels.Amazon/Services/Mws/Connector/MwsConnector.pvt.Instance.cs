// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.pvt.Instance.cs

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Connector
{
    public partial class MwsConnector
    {
        private static MwsConnector _instance;
        private static readonly object Locker = new object();

        private static MwsConnector GetInstance()
        {
            lock( Locker ) {
                return _instance ?? ( _instance = new MwsConnector() );
            }
        }
    }
}