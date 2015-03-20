// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// GenericResponse.cs
// romak_000, 2015-03-20 20:32

using System;
using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Sdk.Common.Operations.Responses
{
    //Todo: Ref: Split and clean GenericResponse
    public partial class GenericResponse<TR, TC> : IAbstractResponse
        where TR : IResponseResult
    {
        private int _level;

        protected GenericResponse( bool isSucces, TC code )
        {
            IsSuccess = isSucces;
            Code = code;
        }

        protected GenericResponse( bool isSucces, TC code, Exception exception )
            : this( isSucces, code )

        {
            Exception = exception;
        }

        protected GenericResponse( bool isSucces, TC code, TR result )
            : this( isSucces, code )
        {
            Result = result;
        }

        protected GenericResponse( bool isSucces, TC code, TR result, IAbstractResponse innerResponse )
            : this( isSucces, code, result )
        {
            InnerResponse = innerResponse;
            InnerResponse.Level = Level + 1;
        }

        protected GenericResponse( bool isSucces, TC code, string details )
            : this( isSucces, code )
        {
            Details = details;
        }

        public TC Code { get; set; }
        public TR Result { get; set; }
        public string Details { get; set; }
        public Exception Exception { get; set; }
        public IAbstractResponse InnerResponse { get; set; }

        public string Autoinfo
        {
            get { return IsSuccess ? GetSuccessAutoinfo() : GetFailedAutoinfo(); }
        }

        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                if( InnerResponse != null ) {
                    InnerResponse.Level = Level + 1;
                }
            }
        }

        public void Check()
        {
            if( !IsSuccess ) {
                throw new ResponseException( this );
            }
        }

        public bool IsSuccess { get; private set; }
    }
}