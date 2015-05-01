// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipSubmissionStatusResponse.cs

using System;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Responses
{
    public class MipSubmissionStatusResponse : MipResponse< MipGetSubmissionStatusResult >, ITaskProceedInfo
    {
        public MipSubmissionStatusResponse() {}

        public MipSubmissionStatusResponse( Exception exception )
            : base( exception ) {}

        public DateTime ProceedTime { get; set; }
    }
}