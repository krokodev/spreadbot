/*
 *  $Id: TypeIdAttribute.cs 144 2010-10-28 12:11:08Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Указывает идентификатор для типа.
    /// Стандартная реализация определителя типов использует для идентификации именно этот атрибут.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TypeIdAttribute : Attribute, IKeyed<string> {
        /// <summary>
        /// Идентификатор типа.
        /// </summary>
        public readonly string Id;

        /// <summary>
        /// Создает новый экземпляр TypeIdAttribute.
        /// </summary>
        public TypeIdAttribute(string typeId) {
            Id = typeId;
        }

        #region IKeyed[System.String] implementation
        string IKeyed<string>.Key {
            get { return Id; }
        }
        #endregion
    }
}

