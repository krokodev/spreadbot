// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipResponse.cs
// Roman, 2015-03-31 1:27 PM

using System;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Sdk.Common.Operations.ResponseResults;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Response
{
    // Todo: Ref: REmove constructors here and in base class, use { initialization }
    public class MipResponse<TR> : GenericResponse< TR, MipOperationStatus >
        where TR : IResponseResult
    {
        public MipResponse() {}

        public MipResponse( bool isSucces, MipOperationStatus code )
            : base( isSucces, code ) {}

        public MipResponse( bool isSucces, MipOperationStatus code, Exception exception )
            : base( isSucces, code, exception ) {}

        public MipResponse( bool isSucces, MipOperationStatus code, TR result )
            : base( isSucces, code, result ) {}

        public MipResponse( bool isSucces, MipOperationStatus code, TR result, IAbstractResponse innerResponse )
            : base( isSucces, code, result, innerResponse ) {}

        public MipResponse( bool isSucces, MipOperationStatus code, string details )
            : base( isSucces, code, details ) {}

    }
}