/*
 *  $Id: ObjectTypeInfo.cs 182 2010-11-28 12:24:42Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;

using Nereal.Extensions;

namespace Nereal.Serialization.TypeInfos {
    /// <summary>
    /// Информация об объектах.
    /// </summary>
    public class ObjectTypeInfo<T> : TypeInfo<T> where T : class {
        private bool _updateableOnly;
        private MemberWrapperList<T> _members;
        private ConstructorDelegate<T> _constructor = null;
        private ParametrizedConstructorDelegate<T> _paramConstructor = null;

        /// <summary>
        /// Создает новый экземпляр информации об объекте.
        /// </summary>
        public ObjectTypeInfo() {
            _updateableOnly = ThisType.HasAttribute<UpdateableOnlyAttribute>();
        }

        /// <summary>
        /// Инициализирует список полей и свойств, а так же конструктор.
        /// </summary>
        protected override void Initialize() {
            _members = new MemberWrapperList<T>();
            if (!_updateableOnly) {
                if (_members.HasConstructorParameters)
                    _paramConstructor = SerializationConfig.AccessorFactory.CreateConstructor<T>(_members.GetParameterTypes());
                else
                    _constructor = SerializationConfig.AccessorFactory.CreateConstructor<T>();
            }
        }

        /// <summary>
        /// Объект может быть обновлен, если тип только обновляемый или имеет конструктор по умолчанию.
        /// </summary>
        public override bool IsUpdateable {
            get { return _updateableOnly || ThisType.GetConstructor(Type.EmptyTypes) != null; }
        }

        /// <summary>
        /// Объект может быть сохранен только в виде элемента.
        /// </summary>
        public override bool IsElement(MemberWrapper member) {
            return true;
        }

        /// <summary>
        /// Создает новый объект путем вызова конструктора по умолчанию.
        /// </summary>
        public override T CreateNew() {
            EnsureInitialize();
            return _constructor != null ? _constructor() : null;
        }

        /// <summary>
        /// Клонирует указанный объект.
        /// На только обновляемые классы клонирование не поддерживается.
        /// </summary>
        public override T Clone(T value) {
            if (_updateableOnly)
                return null;
            EnsureInitialize();
            if (_members.HasConstructorParameters)
                return CloneConstructed(value);
            else
                return CloneDefault(value);
        }

        /// <summary>
        /// Клонирует объект без параметров конструктора.
        /// </summary>
        private T CloneDefault(T value) {
            var newValue = CreateNew();
            foreach (var m in _members.GetMembers())
                m.Copy(value, newValue);
            return newValue;
        }

        /// <summary>
        /// Клонирует объект с параметрами конструктора.
        /// </summary>
        private T CloneConstructed(T value) {
            var parameters = new object[_members.ConstructorParameterCount];
            foreach (var m in _members.GetMembers())
                if (m.IsConstructorParameter)
                    parameters[m.ConstructorParameterNumber] = m.CloneObjectValue(value);
            var newValue = _paramConstructor(parameters);
            foreach (var m in _members.GetMembers())
                if (!m.IsConstructorParameter)
                    m.Copy(value, newValue);
            return newValue;
        }

        /// <summary>
        /// Сравнивает два объекта.
        /// </summary>
        public override bool Equals(T a, T b) {
            if (a == b)
                return true;
            if (a == null || b == null)
                return false;
            EnsureInitialize();
            foreach (var member in _members.GetMembers())
                if (!member.Equals(a, b))
                    return false;
            return true;
        }

        /// <summary>
        /// Проверяет объект на null.
        /// </summary>
        public override bool IsNull(T value) {
            return value == null;
        }

        /// <summary>
        /// Десериализует объект.
        /// На только обновляемые классы прямая десериализация не поддерживается, только обновляющая.
        /// </summary>
        public override T Deserialize(MemberWrapper member, DeserializationContext context) {
            if (_updateableOnly)
                return null;
            EnsureInitialize();
            if (_members.HasConstructorParameters)
                return DeserializeConstructed(member, context);
            else
                return DeserializeDefault(member, context);
        }

        /// <summary>
        /// Десериализует объект без параметров конструктора.
        /// </summary>
        private T DeserializeDefault(MemberWrapper member, DeserializationContext context) {
            var value = CreateNew();
            Deserialize(member, context, value);
            return value;
        }

        /// <summary>
        /// Десериализует объект с параметрами конструктора.
        /// </summary>
        private T DeserializeConstructed(MemberWrapper member, DeserializationContext context) {
            var values = new List<MemberValue<T>>(_members.GetValues(context));
            var parameters = new object[_members.ConstructorParameterCount];
            foreach (var v in values)
                if (v.Member.IsConstructorParameter)
                    parameters[v.Member.ConstructorParameterNumber] = v.Value;
            var value = _paramConstructor(parameters);
            OnBeforeDeserialize(context, value);
            foreach (var v in values)
                if (!v.Member.IsConstructorParameter)
                    v.SetValue(value);
            OnAfterDeserialize(context, value);
            return value;
        }


        /// <summary>
        /// Десериализует существующий объект.
        /// </summary>
        public override void Deserialize(MemberWrapper member, DeserializationContext context, T value) {
            EnsureInitialize();
            OnBeforeDeserialize(context, value);
            foreach (var m in _members.GetMembers(context))
                m.Deserialize(context, value);
            OnAfterDeserialize(context, value);
        }

        /// <summary>
        /// Сериализует объект.
        /// </summary>
        public override void Serialize(MemberWrapper member, SerializationContext context, T value) {
            EnsureInitialize();
            OnBeforeSerialize(context, value);
            foreach (var m in _members.GetMembers())
                m.Serialize(context, value);
            OnAfterSerialize(context, value);
        }
    }

    /// <summary>
    /// Селектор класса информации об объектах.
    /// </summary>
    [TypeInfoSelector]
    public class ObjectTypeInfoSelector : ITypeInfoSelector {
        /// <summary>
        /// Максимально возможный приоритет, так как самый последний селектор.
        /// </summary>
        public int Priority {
            get { return int.MaxValue; }
        }

        /// <summary>
        /// Возвращает true, так как самый последний селектор.
        /// </summary>
        public bool Accept(Type objectType) {
            return true;
        }

        /// <summary>
        /// Получает класс информации об объекте.
        /// </summary>
        public Type GetTypeInfo(Type objectType) {
            return typeof(ObjectTypeInfo<>).MakeGenericType(objectType);
        }
    }
}
