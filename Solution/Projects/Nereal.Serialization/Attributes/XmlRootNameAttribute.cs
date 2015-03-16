/*
 *  $Id: XmlRootNameAttribute.cs 144 2010-10-28 12:11:08Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Указывает xml-имя корневого элемента для (де)сериализации данного класса или любого его потомка.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class XmlRootNameAttribute : Attribute {
        /// <summary>
        /// Xml-имя корневого элемента.
        /// </summary>
        public readonly string RootName;

        /// <summary>
        /// Создает новый экземпляр XmlRootNameAttribute.
        /// </summary>
        public XmlRootNameAttribute(string rootName) {
            RootName = rootName;
        }
    }
}

