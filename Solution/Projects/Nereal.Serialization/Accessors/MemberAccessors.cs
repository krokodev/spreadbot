/*
 *  $Id: MemberAccessors.cs 114 2010-10-23 12:00:38Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Reflection;

namespace Nereal.Serialization {
    /// <summary>
    /// Структура для единого хранения четырех делегатов доступа к полю или свойству.
    /// </summary>
    public struct MemberAccessors<T, V> {
        /// <summary>
        /// Делегат получения значения.
        /// </summary>
        public GetterDelegate<T, V> Getter;
        /// <summary>
        /// Делегат получения значения с передачей объекта по ссылке.
        /// </summary>
        public RefGetterDelegate<T, V> RefGetter;
        /// <summary>
        /// Делегат установки значения.
        /// </summary>
        public SetterDelegate<T, V> Setter;
        /// <summary>
        /// Делегат установки значения с передачей объекта по ссылке.
        /// </summary>
        public RefSetterDelegate<T, V> RefSetter;

        /// <summary>
        /// Создает набор делегатов доступа с передачей объекта по значению.
        /// </summary>
        public MemberAccessors(GetterDelegate<T, V> getter, SetterDelegate<T, V> setter) {
            Getter = getter;
            RefGetter = null;
            Setter = setter;
            RefSetter = null;
        }

        /// <summary>
        /// Создает набор делегатов доступа с передачей объекта по ссылке.
        /// </summary>
        public MemberAccessors(RefGetterDelegate<T, V> getter, RefSetterDelegate<T, V> setter) {
            Getter = null;
            RefGetter = getter;
            Setter = null;
            RefSetter = setter;
        }
    }
}
