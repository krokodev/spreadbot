/*
 *  $Id: TypeInfoAttribute.cs 144 2010-10-28 12:11:08Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Указывает тип, для которого должен быть зарегистрирован данный TypeInfo-класс.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TypeInfoAttribute : Attribute {
        /// <summary>
        /// Тип, описываемый данным TypeInfo.
        /// </summary>
        public readonly Type InfoType;

        /// <summary>
        /// Создает новый экземпляр TypeInfoAttribute.
        /// </summary>
        public TypeInfoAttribute(Type infoType) {
            InfoType = infoType;
        }
    }
}
