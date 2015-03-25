// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// GenericResponse.pvt.Descriptions.cs
// romak_000, 2015-03-25 15:25

using System;
using MoreLinq;

namespace Spreadbot.Sdk.Common.Operations.Responses
{
    public partial class GenericResponse<TR, TC>
    {
        // ===================================================================================== []
        // Autoinfo
        private string GetSuccessAutoinfo()
        {
            return AutoinfoResponse(
                AutoinfoField( "Result", Result ),
                AutoinfoField( "Details", Details ),
                AutoinfoInnerResponse( InnerResponse )
                );
        }

        // --------------------------------------------------------[]
        private string GetFailedAutoinfo()
        {
            return AutoinfoResponse(
                AutoinfoException( Exception ),
                AutoinfoField( "Details", Details ),
                AutoinfoInnerResponse( InnerResponse )
                );
        }

        // ===================================================================================== []
        // Elements
        private string AutoinfoField( string name, object value )
        {
            if( value == null ) {
                return "";
            }

            return string.Format( "{0}{1}: [{2}]", NewLine( Level ), name, value );
        }

        // --------------------------------------------------------[]
        private string AutoinfoException( Exception e )
        {
            if( e == null ) {
                return null;
            }

            return AutoinfoSection(
                "Exception",
                AutoinfoField( "Type", e.GetType() ),
                AutoinfoField( "Message", ExceptionMessage( e ) ),
                AutoinfoField( "StackTrace", e.StackTrace ),
                AutoinfoField(
                    "InnerException",
                    AutoinfoException( e.InnerException )
                    )
                );
        }

        // --------------------------------------------------------[]
        private string ExceptionMessage( Exception e )
        {
            var responseException = e as ResponseException;
            if( responseException != null ) {
                return responseException.Response.Autoinfo;
            }
            return e.Message;
        }

        // --------------------------------------------------------[]
        private string AutoinfoInnerResponse( IAbstractResponse response )
        {
            if( response == null ) {
                return null;
            }

            return AutoinfoSection(
                "InnerResponse",
                response.Autoinfo
                );
        }

        // --------------------------------------------------------[]
        private string AutoinfoSection( string sectionName, params string[] args )
        {
            var sectionContent = "";
            args.ForEach(
                arg => { sectionContent = string.Format( "{0}{1}", sectionContent, arg ); } );

            return string.Format( "{0}{1}:{0}[{2}{0}]",
                NewLine( Level ),
                sectionName,
                sectionContent
                );
        }

        // --------------------------------------------------------[]
        private string AutoinfoResponse( params string[] args )
        {
            return AutoinfoSection( Code.ToString(), args );
        }

        // --------------------------------------------------------[]
        private static string NewLine( int level )
        {
            return "\n" + ( level == 0 ? "" : new string( ' ', 2*level ) );
        }
    }
}