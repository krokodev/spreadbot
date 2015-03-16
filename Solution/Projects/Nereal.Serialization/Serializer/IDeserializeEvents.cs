/*
 *  $Id: IDeserializeEvents.cs 191 2010-12-02 15:45:28Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

namespace Nereal.Serialization {
    /// <summary>
    /// Интерфейс, позволяющий определить своё поведение при десериализации.
    /// FIXME : На данный момент для добавления/удаления ReferenceCollector в контекст.
    /// </summary>
    public interface IDeserializeEvents {
        /// <summary>
        /// Выполняет действия перед десериализацией данного объекта (но после его конструирования).
        /// </summary>
        void OnBeforeDeserialize(DeserializationContext context);
        /// <summary>
        /// Выполняет действия после десериализации данного объекта.
        /// </summary>
        void OnAfterDeserialize(DeserializationContext context);
    }
}
