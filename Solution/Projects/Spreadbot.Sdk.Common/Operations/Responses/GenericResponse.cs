// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// GenericResponse.cs
// romak_000, 2015-03-26 20:21

using System;
using System.Dynamic;
using Spreadbot.Sdk.Common.Crocodev.Common;
using Spreadbot.Sdk.Common.Operations.ResponseResults;
using YamlDotNet.Serialization;

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
        [YamlMember( Order = -1 )]
        public string Type
        {
            get { return GetType().ToString(); }

            // ReSharper disable once ValueParameterNotUsed
            set { }
        }

        [YamlMember( Order = 0 )]
        public TC Code { get; set; }

        [YamlMember( Order = 1 )]
        public bool IsSuccess { get; set; }

        [YamlMember( Order = 2 )]
        public TR Result { get; set; }

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