// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.pvt.Product.cs

using Spreadbot.Core.Channels.Amazon.Services.Mws.Results;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Connector
{
    public partial class MwsConnector {
        private Response< MwsGetProductInfoResult > _GetProductInfo( string sku )
        {
            // Todo:> Use https://developer.amazonservices.com/gp/mws/api.html/187-7895935-6745567?ie=UTF8&group=products&section=products&version=latest
            // Compile it into Spreadbot.common from t:\MWSProductsCSharpClientLibrary-2011-10-01._V330254532_.zip
            throw new System.NotImplementedException();
        }
    }
}