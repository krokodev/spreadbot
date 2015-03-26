// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// GenericResponse.cs
// romak_000, 2015-03-26 18:13

using System;
using System.Dynamic;
using Spreadbot.Sdk.Common.Crocodev.Common;
using Spreadbot.Sdk.Common.Operations.ResponseResults;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Spreadbot.Sdk.Common.Operations.Responses
{
    public class GenericResponse<TR, TC> : IAbstractResponse
        where TR : IResponseResult
    {
        // --------------------------------------------------------[]
        protected GenericResponse( bool isSucces, TC code )
        {
            IsSuccess = isSucces;
            Code = code;
        }

        // --------------------------------------------------------[]
        protected GenericResponse( bool isSucces, TC code, Exception exception )
            : this( isSucces, code )
        {
            ExceptionInfo = GetExceptionInfo( exception );
        }

        // --------------------------------------------------------[]
        protected GenericResponse( bool isSucces, TC code, TR result )
            : this( isSucces, code )
        {
            Result = result;
        }

        protected GenericResponse( bool isSucces, TC code, TR result, IAbstractResponse innerResponse )
            : this( isSucces, code, result )
        {
            InnerResponse = innerResponse;
        }

        protected GenericResponse( bool isSucces, TC code, string details )
            : this( isSucces, code )
        {
            Details = details;
        }

        protected GenericResponse() {}

        // --------------------------------------------------------[]
        // Code: GenericResponse.Yaml
        public TC Code { get; set; }
        public bool IsSuccess { get; set; }
        public TR Result { get; set; }
        public string Details { get; set; }
        public dynamic ExceptionInfo { get; set; }
        public IAbstractResponse InnerResponse { get; set; }

        // --------------------------------------------------------[]
        public void Check()
        {
            if( !IsSuccess ) {
                throw new ResponseException( this );
            }
        }

        // --------------------------------------------------------[]
        public override string ToString()
        {
            return this.ToYamlString();
        }

        // --------------------------------------------------------[]
        private static dynamic GetExceptionInfo( Exception exception )
        {
            // Code: PrepareExceptionInfo
            dynamic exceptionInfo = new ExpandoObject();

            exceptionInfo.Type = exception.GetType().ToString();
            exceptionInfo.Message = exception.Message;
            exceptionInfo.StackTrace = exception.StackTrace;
            exceptionInfo.Source = exception.Source;

            if( exception.InnerException != null ) {
                exceptionInfo.InnerException = GetExceptionInfo( exception.InnerException );
            }

            return exceptionInfo;
        }
    }
}