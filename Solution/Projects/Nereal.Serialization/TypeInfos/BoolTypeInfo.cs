/*
 *  $Id: BoolTypeInfo.cs 191 2010-12-02 15:45:28Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization.TypeInfos {
    /// <summary>
    /// Информация о типе Boolean.
    /// </summary>
    [TypeInfo(typeof(bool))]
    public class BoolTypeInfo : ValueTypeInfo<bool> {
        private static readonly string[] TrueValues = { "true", "1", "True" };
        private static readonly string[] FalseValues = { "false", "0", "False" };

        /// <summary>
        /// Конвертирует строку в bool.
        /// </summary>
        public override bool ConvertFromString(string value) {
            return Array.IndexOf(TrueValues, value) != -1;
        }

        /// <summary>
        /// Конвертирует bool в строку.
        /// </summary>
        public override string ConvertToString(bool value) {
            var values = value ? TrueValues : FalseValues;
            return values[0];
        }
    }
}
