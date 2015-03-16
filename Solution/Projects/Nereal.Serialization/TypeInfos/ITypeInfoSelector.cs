/*
 *  $Id: ITypeInfoSelector.cs 120 2010-10-23 22:42:04Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Интерфейс селектора класса информации о типе.
    /// Определяет, подходит ли тип в селектор, и создает для него класс информации.
    /// </summary>
    public interface ITypeInfoSelector {
        /// <summary>
        /// Приоритет селектора.
        /// </summary>
        int Priority { get; }
        /// <summary>
        /// Проверяет, подходит ли указанный тип для данного селектора.
        /// </summary>
        bool Accept(Type objectType);
        /// <summary>
        /// Получает класс информации для указанного типа.
        /// </summary>
        Type GetTypeInfo(Type objectType);
    }
}
