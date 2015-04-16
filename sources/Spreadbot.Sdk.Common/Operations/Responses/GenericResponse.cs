// Spreadbot (c) 2015 Krokodev
// Spreadbot.Sdk.Common
// GenericResponse.cs

using System;
using System.Collections.Generic;
using System.Dynamic;
using Spreadbot.Sdk.Common.Crocodev.Common;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.ResponseResults;
using YamlDotNet.Serialization;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Spreadbot.Sdk.Common.Operations.Responses
{
    public class GenericResponse<TResult, TStatusCode> : IAbstractResponse
        where TResult : IResponseResult
    {
        protected GenericResponse()
        {
            IsSuccess = true;
            InnerResponses = new List< IAbstractResponse >();
        }

        protected GenericResponse( Exception exception )
            : this()
        {
            IsSuccess = false;
            ExceptionInfo = GetExceptionInfo( exception );
        }

        // --------------------------------------------------------[]
        [YamlMember( Order = -1 )]
        public string Type
        {
            get { return GetType().ToString(); }
        }

        [YamlMember( Order = 0 )]
        public TStatusCode StatusCode { get; set; }

        [YamlMember( Order = 1 )]
        public bool IsSuccess { get; set; }

        [YamlMember( Order = 2 )]
        public string ArgsInfo { get; set; }

        [YamlMember( Order = 3 )]
        public TResult Result { get; set; }

        [YamlMember( Order = 4 )]
        public string Details { get; set; }

        [YamlMember( Order = 5 )]
        public string ExceptionInfo { get; set; }

        [YamlMember( Order = 6 )]
        public List< IAbstractResponse > InnerResponses { get; set; }

        // --------------------------------------------------------[]
        public void Check()
        {
            if( !IsSuccess ) {
                throw new SpreadbotResponseException( this );
            }
        }

        // --------------------------------------------------------[]
        public override string ToString()
        {
            return this.ToYamlString();
        }

        // --------------------------------------------------------[]
        private static string GetExceptionInfo( Exception exception )
        {
            dynamic exceptionInfo = new ExpandoObject();

            exceptionInfo.Type = exception.GetType().ToString();
            exceptionInfo.Message = exception.Message;

            if( exception is ISpreadbotDetaledException ) {
                exceptionInfo.Details = ( ( ISpreadbotDetaledException ) exception ).GetDetails();
            }
            if( exception.InnerException != null ) {
                exceptionInfo.InnerException = GetExceptionInfo( exception.InnerException );
            }

            return YamlUtils.MakeYamlString( exceptionInfo );
        }
    }
}