// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// GenericResponse.cs
// Roman, 2015-04-01 4:59 PM

using System;
using System.Dynamic;
using Nereal.Serialization;
using Spreadbot.Sdk.Common.Crocodev.Common;
using Spreadbot.Sdk.Common.Operations.ResponseResults;
using YamlDotNet.Serialization;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Spreadbot.Sdk.Common.Operations.Responses
{
    // Todo: Ref: Remove constructors GenericResponse, use { initialization }

    public class GenericResponse<TResult, TStatusCode> : IAbstractResponse
        where TResult : IResponseResult
    {
 /*       // --------------------------------------------------------[]
        protected GenericResponse( bool isSucces, TStatusCode statusCode )
        {
            IsSuccess = isSucces;
            StatusCode = statusCode;
        }

        // --------------------------------------------------------[]
        protected GenericResponse( bool isSucces, TStatusCode statusCode, Exception exception )
            : this( isSucces, statusCode )
        {
            ExceptionInfo = GetExceptionInfo( exception );
        }

        // --------------------------------------------------------[]
        protected GenericResponse( bool isSucces, TStatusCode statusCode, TResult result )
            : this( isSucces, statusCode )
        {
            Result = result;
        }

        protected GenericResponse( bool isSucces, TStatusCode statusCode, TResult result, IAbstractResponse innerResponse )
            : this( isSucces, statusCode, result )
        {
            InnerResponse = innerResponse;
        }

        protected GenericResponse( bool isSucces, TStatusCode statusCode, string details )
            : this( isSucces, statusCode )
        {
            Details = details;
        }
        */

        protected GenericResponse()
        {
            IsSuccess = true;
        }

        protected GenericResponse( Exception exception )
        {
            IsSuccess = false;
            ExceptionInfo = GetExceptionInfo( exception );
        }

        // --------------------------------------------------------[]
        [YamlMember( Order = -1 )]
        public string Type
        {
            get { return GetType().ToString(); }

            // Todo: Ref: remove GenericResponse Type.Set
            // ReSharper disable once ValueParameterNotUsed
            //set { }
        }

        [YamlMember( Order = 0 )]
        public TStatusCode StatusCode { get; set; }

        [YamlMember( Order = 1 )]
        public bool IsSuccess { get; set; }

        [YamlMember( Order = 2 )]
        public TResult Result { get; set; }

        [YamlMember( Order = 3 )]
        public string Details { get; set; }

        [YamlMember( Order = 4 )]
        public dynamic ExceptionInfo { get; set; }

        [YamlMember( Order = 5 )]
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
            dynamic exceptionInfo = new ExpandoObject();

            exceptionInfo.Type = exception.GetType().ToString();
            exceptionInfo.Message = exception.Message;

            //exceptionInfo.StackTrace = exception.StackTrace;
            //exceptionInfo.Source = exception.Source;

            if( exception.InnerException != null ) {
                exceptionInfo.InnerException = GetExceptionInfo( exception.InnerException );
            }

            return exceptionInfo;
        }
    }
}