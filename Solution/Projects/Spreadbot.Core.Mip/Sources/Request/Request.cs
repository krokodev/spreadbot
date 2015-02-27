using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Crocodev.Common.Identifier;

namespace Spreadbot.Core.Mip
{
    public class Request : Identifiable<Request, Guid>
    {
        public static Identifier GenerateRequestId()
        {
            return (Identifier)new Guid();
        }
        public static bool VerifyRequestId(Identifier requestId)
        {
            Guid guid;
            return Guid.TryParse(requestId.ToString(), out guid);
        }
    }
}