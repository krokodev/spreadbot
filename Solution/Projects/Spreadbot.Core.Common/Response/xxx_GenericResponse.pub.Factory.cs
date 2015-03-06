/*using System;

namespace Spreadbot.Core.Common
{
    public partial class GenericResponse<TR,TC>
    {
        // ===================================================================================== []
        // Failed Responses
        public static GenericResponse<TR,TC> NewFail(TC statusCode, string details)
        {
            return new GenericResponse<TR,TC>(false, statusCode)
            {
                Details = details
            };
        }

        // --------------------------------------------------------[]
        public static GenericResponse<TR,TC> NewFail(TC statusCode, Exception e)
        {
            return new GenericResponse<TR,TC>(false, statusCode)
            {
                Exception =  e
            };
        }

        // ===================================================================================== []
        // Successful Responses
        public static GenericResponse<TR, TC> NewSuccess(TC statusCode, TR result)
        {
            return new GenericResponse<TR, TC>(true, statusCode)
            {
                Result = result
            };
        }
        // --------------------------------------------------------[]
        public static GenericResponse<TR, TC> NewSuccess(TC statusCode, TR result, IResponse innerResponse)
        {
            return new GenericResponse<TR, TC>(true, statusCode)
            {
                Result = result,
                InnerResponse = innerResponse
            };
        }
        // --------------------------------------------------------[]
        public static GenericResponse<TR, TC> NewSuccess(TC statusCode, TR result, string details)
        {
            return new GenericResponse<TR, TC>(true, statusCode)
            {
                Details = details,
                Result = result
            };
        }
    }
}*/