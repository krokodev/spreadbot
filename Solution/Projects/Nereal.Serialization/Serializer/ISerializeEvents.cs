/*
 *  $Id: ISerializeEvents.cs 191 2010-12-02 15:45:28Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

namespace Nereal.Serialization {
    /// <summary>
    /// Интерфейс, позволяющий определить своё поведение при сериализации.
    /// FIXME : На данный момент для добавления/удаления ReferenceCollector в контекст.
    /// </summary>
    public interface ISerializeEvents {
        /// <summary>
        /// Выполняет действия перед сериализацией данного объекта.
        /// </summary>
        void OnBeforeSerialize(SerializationContext context);
        /// <summary>
        /// Выполняет действия после сериализации данного объекта.
        /// </summary>
        void OnAfterSerialize(SerializationContext context);
    }
}
