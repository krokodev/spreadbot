/*
 *  $Id: IAccessorFactory.cs 106 2010-10-22 11:26:52Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Reflection;

namespace Nereal.Serialization {
    /// <summary>
    /// Интерфейс для фабрики делегатов доступа.
    /// </summary>
    public interface IAccessorFactory {
        /// <summary>
        /// Создает делегат получения значения поля.
        /// </summary>
        GetterDelegate<T, V> CreateGetter<T, V>(FieldInfo field);
        /// <summary>
        /// Создает делегат получения значения свойства.
        /// </summary>
        GetterDelegate<T, V> CreateGetter<T, V>(PropertyInfo property);

        /// <summary>
        /// Создает делегат получения значения поля с передачей объекта по ссылке.
        /// </summary>
        RefGetterDelegate<T, V> CreateRefGetter<T, V>(FieldInfo field);
        /// <summary>
        /// Создает делегат получения значения свойства с передачей объекта по ссылке.
        /// </summary>
        RefGetterDelegate<T, V> CreateRefGetter<T, V>(PropertyInfo property);

        /// <summary>
        /// Создает делегат установки значения поля.
        /// </summary>
        SetterDelegate<T, V> CreateSetter<T, V>(FieldInfo field);
        /// <summary>
        /// Создает делегат установки значения свойства.
        /// </summary>
        SetterDelegate<T, V> CreateSetter<T, V>(PropertyInfo property);

        /// <summary>
        /// Создает делегат установки значения поля с передачей объекта по ссылке.
        /// </summary>
        RefSetterDelegate<T, V> CreateRefSetter<T, V>(FieldInfo field);
        /// <summary>
        /// Создает делегат установки значения свойства с передачей объекта по ссылке.
        /// </summary>
        RefSetterDelegate<T, V> CreateRefSetter<T, V>(PropertyInfo property);

        /// <summary>
        /// Создает делегат конструирования объекта без параметров.
        /// </summary>
        ConstructorDelegate<T> CreateConstructor<T>() where T : class;
        /// <summary>
        /// Создает делегат конструирования объекта с параметрами указанных типов.
        /// </summary>
        ParametrizedConstructorDelegate<T> CreateConstructor<T>(Type[] parameterTypes);
    }
}

