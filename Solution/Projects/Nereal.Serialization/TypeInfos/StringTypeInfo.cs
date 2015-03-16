/*
 *  $Id: StringTypeInfo.cs 190 2010-12-02 13:20:50Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization.TypeInfos {
    /// <summary>
    /// Информация о типе String.
    /// </summary>
    [TypeInfo(typeof(string))]
    public class StringTypeInfo : TypeInfo<string> {
        /// <summary>
        /// Строки не могут иметь потомков.
        /// </summary>
        public override bool IsSealed {
            get { return true; }
        }

        /// <summary>
        /// Возвращает пустую строку.
        /// </summary>
        public override string CreateNew() {
            return string.Empty;
        }

        /// <summary>
        /// Возвращает саму же строку, вместо клонирования.
        /// </summary>
        public override string Clone(string value) {
            return value;
        }

        /// <summary>
        /// Сравнивает две строки.
        /// </summary>
        public override bool Equals(string a, string b) {
            return a == b;
        }

        /// <summary>
        /// Проверяет строку на null.
        /// </summary>
        public override bool IsNull(string value) {
            return value == null;
        }

        /// <summary>
        /// Проверяет необходимость сериализации строки по неравенству значению по умолчанию.
        /// </summary>
        public override bool NeedSerialize(string value, bool hasDefaultValue, string defaultValue) {
            return value != null && (!hasDefaultValue || value != defaultValue);
        }

        /// <summary>
        /// Возвращает саму же строку, вместо конвертации.
        /// </summary>
        public override string ConvertFromString(string value) {
            return value;
        }

        /// <summary>
        /// Возвращает саму же строку, вместо конвертации.
        /// </summary>
        public override string ConvertToString(string value) {
            return value;
        }
    }
}
