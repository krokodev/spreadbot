// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// EnumUtil.cs
// Roman, 2015-04-01 9:11 PM

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Spreadbot.Sdk.Common.Crocodev.Common
{
    public static class EnumUtil
    {
        public static IEnumerable< T > GetValues<T>()
            where T : struct, IComparable, IFormattable, IConvertible
        {
            var type = typeof( T );
            if( !type.IsEnum ) {
                throw new InvalidCastException(
                    string.Format( "Type '{0}' isn't an enum.", type.Name ) );
            }
            return Enum.GetValues( type ).Cast< T >();
        }

        public static IEnumerable< string > GetStringValues<T>()
            where T : struct, IComparable, IFormattable, IConvertible
        {
            return GetValues< T >().Select( e => e.ToString( CultureInfo.InvariantCulture ) );
        }
    }
}