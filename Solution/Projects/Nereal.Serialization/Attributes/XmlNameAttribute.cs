/*
 *  $Id: XmlNameAttribute.cs 144 2010-10-28 12:11:08Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Указывает xml-имя для поля или свойства. Опционально указывает xml-имя для элементов массивов/списков/словарей.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class XmlNameAttribute : Attribute {
        /// <summary>
        /// Xml-имя поля или свойства.
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// Xml-имя элемента массива/списка/словаря.
        /// </summary>
        public readonly string ItemName;

        /// <summary>
        /// Создает новый экземпляр XmlNameAttribute без указания имени элемента.
        /// </summary>
        public XmlNameAttribute(string name) : this(name, null) {
        }
        /// <summary>
        /// Создает новый экземпляр XmlNameAttribute с указанием имени элемента.
        /// </summary>
        /// <param name="name">
        /// A <see cref="System.String"/>
        /// </param>
        /// <param name="itemName">
        /// A <see cref="System.String"/>
        /// </param>
        public XmlNameAttribute(string name, string itemName) {
            Name = name;
            ItemName = itemName;
        }
    }
}
