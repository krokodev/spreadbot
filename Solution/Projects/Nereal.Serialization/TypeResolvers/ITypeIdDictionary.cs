/*
 *  $Id: ITypeIdDictionary.cs 184 2010-11-28 12:51:22Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Интерфейс для доступа к словаю идентификаторов типов.
    /// </summary>
    public interface ITypeIdDictionary {
        /// <summary>
        /// Проверяет наличие типа в словаре.
        /// </summary>
        bool ContainsType(Type type);
        /// <summary>
        /// Проверяет наличие идентификатора в словаре.
        /// </summary>
        bool ContainsId(string id);
        /// <summary>
        /// Получает идентификатор типа.
        /// </summary>
        string GetId(Type type);
        /// <summary>
        /// Получает тип по идентификатору.
        /// </summary>
        Type GetType(string id);
    }
}
