// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels.Ebay
// MipRequestStatusResponse.cs

using System;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Response
{
    public class MipRequestStatusResponse : MipResponse< MipGetRequestStatusResult >, ITaskProceedInfo
    {
        public MipRequestStatusResponse() {}

        public MipRequestStatusResponse( Exception exception )
            : base( exception ) {}

        public DateTime ProceedTime { get; set; }
    }
}