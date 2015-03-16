/*
 *  $Id: ITypeResolver.cs 113 2010-10-23 11:53:04Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Интерфейс сохранения и восстановления типов объектов (определитель типов).
    /// </summary>
    public interface ITypeResolver {
        /// <summary>
        /// Восстановление типа по контексту десериализации.
        /// </summary>
        Type Deserialize(DeserializationContext context, Type baseType);

        /// <summary>
        /// Определение имени сохраняемого элемента по типу.
        /// Если null, то тип не определяет имя элемента.
        /// </summary>
        string GetTypeElementName(Type objectType);

        /// <summary>
        /// Сохранение типа в контекст сериализации.
        /// </summary>
        void Serialize(SerializationContext context, Type baseType, Type objectType);
    }
}

