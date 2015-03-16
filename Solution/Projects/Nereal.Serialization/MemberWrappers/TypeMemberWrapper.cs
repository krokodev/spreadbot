/*
 *  $Id: TypeMemberWrapper.cs 187 2010-11-29 14:52:37Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;
using System.Reflection;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Абстрактная обертка над полем или свойством с указанным типом-владельцем.
    /// Позволяет операции с участием типа-владельца без учета типа значения.
    /// </summary>
    public abstract class TypeMemberWrapper<T> : MemberWrapper {
        private int _constructorParameterNumber;

        internal TypeMemberWrapper(MemberInfoEx member) : base(member) {
            UpdateConstructorParameter();
        }

        #region Constructor parameter
        /// <summary>
        /// Является ли поле/свойство параметром конструктора.
        /// </summary>
        public bool IsConstructorParameter {
            get { return _constructorParameterNumber >= 0; }
        }
        /// <summary>
        /// Номер среди параметров конструктора.
        /// </summary>
        public int ConstructorParameterNumber {
            get { return _constructorParameterNumber; }
        }

        /// <summary>
        /// Обновляет свойства параметра конструктора.
        /// </summary>
        private void UpdateConstructorParameter() {
            var info = SerializationConfig.ConstructorManager[typeof(T)];
            _constructorParameterNumber = info.Find(MemberInfo);
        }
        #endregion

        #region Abstract methods
        /// <summary>
        /// Получает значение поля/свойства в виде object.
        /// </summary>
        public abstract object GetObjectValue(T obj);
        /// <summary>
        /// Получает значение поля/свойства в виде object, с передачей объекта по ссылке (для структур).
        /// </summary>
        public abstract object GetObjectValue(ref T obj);

        /// <summary>
        /// Получает копию значения поля/свойства в виде object.
        /// </summary>
        public abstract object CloneObjectValue(T obj);
        /// <summary>
        /// Получает копию значения поля/свойства в виде object, с передачей объекта по ссылке (для структур).
        /// </summary>
        public abstract object CloneObjectValue(ref T obj);

        /// <summary>
        /// Копирует поле/свойство из одного объекта в другой.
        /// </summary>
        public abstract void Copy(T src, T dest);
        /// <summary>
        /// Копирует поле/свойство из одного объекта в другой, с передачей объектов по ссылке (для структур).
        /// </summary>
        public abstract void Copy(ref T src, ref T dest);

        /// <summary>
        /// Сравнивает поле/свойство из двух объектов.
        /// </summary>
        public abstract bool Equals(T a, T b);
        /// <summary>
        /// Сравнивает поле/свойство из двух объектов, с передачей объектов по ссылке (для структур).
        /// </summary>
        public abstract bool Equals(ref T a, ref T b);

        /// <summary>
        /// Десериализует поля/свойства и возвращает его в виде обертки над значением.
        /// </summary>
        public abstract MemberValue<T> DeserializeMemberValue(DeserializationContext context);

        /// <summary>
        /// Десериализует поля/свойства и устанавливает его в объект.
        /// </summary>
        public abstract void Deserialize(DeserializationContext context, T obj);
        /// <summary>
        /// Десериализует поля/свойства и устанавливает его в объект, с передачей объекта по ссылке (для структур).
        /// </summary>
        public abstract void Deserialize(DeserializationContext context, ref T obj);

        /// <summary>
        /// Сериализует поля/свойства.
        /// </summary>
        public abstract void Serialize(SerializationContext context, T obj);
        /// <summary>
        /// Сериализует поля/свойства, с передачей объекта по ссылке (для структур).
        /// </summary>
        public abstract void Serialize(SerializationContext context, ref T obj);
        #endregion
    }
}
