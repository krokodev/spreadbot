/*
 *  $Id: DefaultValueAttribute.cs 161 2010-11-06 22:59:31Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Указывает полям и свойствам значение по умолчанию.
    /// Если значением атрибута является строка, то она будет сконвертирована в тип поля/свойства с помощью соответствующего TypeInfo.
    /// При сериализации, если значение равно значению по умолчанию, то оно не будет сохраняться.
    /// При десериализации, если это поле или свойство отсутствует, то будет установлено значение по умолчанию.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class DefaultValueAttribute : Attribute {
        /// <summary>
        /// Значение по умолчанию.
        /// </summary>
        public readonly object DefaultValue;

        /// <summary>
        /// Создает новый экземпляр DefaultValueAttribute.
        /// </summary>
        public DefaultValueAttribute(object defaultValue) {
            DefaultValue = defaultValue;
        }
    }
}

