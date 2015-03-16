/*
 *  $Id: KeyNameAttribute.cs 144 2010-10-28 12:11:08Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Определяет xml-имя ключа-идентификатора для сериализации словарей.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class KeyNameAttribute : Attribute {
        /// <summary>
        /// Xml-имя ключа-идентификатора.
        /// </summary>
        public readonly string KeyName;

        /// <summary>
        /// Создает новый экземпляр KeyNameAttribute.
        /// </summary>
        public KeyNameAttribute(string keyName) {
            KeyName = keyName;
        }
    }
}
