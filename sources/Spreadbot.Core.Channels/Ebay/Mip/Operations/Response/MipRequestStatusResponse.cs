// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipRequestStatusResponse.cs
// Roman, 2015-04-01 4:59 PM

using System;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Sdk.Common.Operations.Responses;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Response
{
    public class MipRequestStatusResponse : MipResponse< MipGetRequestStatusResult >, ITaskProceedInfo
    {
        public MipRequestStatusResponse() {}

        public MipRequestStatusResponse( bool isSucces, MipOperationStatus code )
            : base( isSucces, code ) {}

        public MipRequestStatusResponse( bool isSucces, MipOperationStatus code, Exception exception )
            : base( isSucces, code, exception ) {}

        public MipRequestStatusResponse( bool isSucces, MipOperationStatus code, MipGetRequestStatusResult result )
            : base( isSucces, code, result ) {}

        public MipRequestStatusResponse(
            bool isSucces,
            MipOperationStatus code,
            MipGetRequestStatusResult result,
            IAbstractResponse innerResponse )
            : base( isSucces, code, result, innerResponse ) {}

        public MipRequestStatusResponse( bool isSucces, MipOperationStatus code, string details )
            : base( isSucces, code, details ) {}

        public DateTime ProceedTime { get; set; }
    }
}