/*
 *  $Id: StructTypeInfo.cs 190 2010-12-02 13:20:50Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;

namespace Nereal.Serialization.TypeInfos {
    /// <summary>
    /// Информация о структурах.
    /// </summary>
    public class StructTypeInfo<T> : TypeInfo<T> where T : struct {
        private MemberWrapperList<T> _members;
        private ParametrizedConstructorDelegate<T> _paramConstructor = null;

        /// <summary>
        /// Инициализирует списка полей и свойств, а так же конструктор.
        /// </summary>
        protected override void Initialize() {
            _members = new MemberWrapperList<T>();
            if (_members.HasConstructorParameters)
                _paramConstructor = SerializationConfig.AccessorFactory.CreateConstructor<T>(_members.GetParameterTypes());
        }

        /// <summary>
        /// Структуры не могут иметь потомков.
        /// </summary>
        public override bool IsSealed {
            get { return true; }
        }

        /// <summary>
        /// Структура может быть сохранена только в виде элемента.
        /// </summary>
        public override bool IsElement(MemberWrapper member) {
            return true;
        }

        /// <summary>
        /// Создает новую структуру.
        /// </summary>
        public override T CreateNew() {
            return new T();
        }

        /// <summary>
        /// Проверяет необходимость сериализации по неравенству значению по умолчанию.
        /// </summary>
        public override bool NeedSerialize(T value, bool hasDefaultValue, T defaultValue) {
            return true;
        }

        /// <summary>
        /// Клонирует структуру.
        /// </summary>
        public override T Clone(T value) {
            EnsureInitialize();
            if (_members.HasConstructorParameters)
                return CloneConstructed(value);
            else
                return CloneDefault(value);
        }

        /// <summary>
        /// Клонирует структуру без параметров конструктора.
        /// </summary>
        private T CloneDefault(T value) {
            var newValue = new T();
            foreach (var m in _members.GetMembers())
                m.Copy(ref value, ref newValue);
            return newValue;
        }

        /// <summary>
        /// Клонирует структуру с параметрами конструктора.
        /// </summary>
        private T CloneConstructed(T value) {
            var parameters = new object[_members.ConstructorParameterCount];
            foreach (var m in _members.GetMembers())
                if (m.IsConstructorParameter)
                    parameters[m.ConstructorParameterNumber] = m.CloneObjectValue(ref value);
            var newValue = _paramConstructor(parameters);
            foreach (var m in _members.GetMembers())
                if (!m.IsConstructorParameter)
                    m.Copy(ref value, ref newValue);
            return newValue;
        }

        /// <summary>
        /// Сравнивает две структуры.
        /// </summary>
        public override bool Equals(T a, T b) {
            EnsureInitialize();
            foreach (var member in _members.GetMembers())
                if (!member.Equals(ref a, ref b))
                    return false;
            return true;
        }

        /// <summary>
        /// Десериализует структуры.
        /// </summary>
        public override T Deserialize(MemberWrapper member, DeserializationContext context) {
            EnsureInitialize();
            if (_members.HasConstructorParameters)
                return DeserializeConstructed(member, context);
            else
                return DeserializeDefault(member, context);
        }

        /// <summary>
        /// Десериализует структуры без параметров конструктора.
        /// </summary>
        private T DeserializeDefault(MemberWrapper member, DeserializationContext context) {
            var value = new T();
            OnBeforeDeserialize(context, value);
            foreach (var m in _members.GetMembers(context))
                m.Deserialize(context, ref value);
            OnAfterDeserialize(context, value);
            return value;
        }

        /// <summary>
        /// Десериализует структуры с параметрами конструктора.
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
                    v.SetValue(ref value);
            OnAfterDeserialize(context, value);
            return value;
        }

        /// <summary>
        /// Сериализует структуры.
        /// </summary>
        /// <param name="member">
        /// A <see cref="MemberWrapper"/>
        /// </param>
        /// <param name="context">
        /// A <see cref="SerializationContext"/>
        /// </param>
        /// <param name="value">
        /// A <see cref="T"/>
        /// </param>
        public override void Serialize(MemberWrapper member, SerializationContext context, T value) {
            EnsureInitialize();
            OnBeforeSerialize(context, value);
            foreach (var m in _members.GetMembers())
                m.Serialize(context, ref value);
            OnAfterSerialize(context, value);
        }
    }

    /// <summary>
    /// Селектор класса информации о структурах.
    /// </summary>
    [TypeInfoSelector]
    public class StructTypeInfoSelector : ITypeInfoSelector {
        /// <summary>
        /// Второй приоритет (значимые типы).
        /// </summary>
        public int Priority {
            get { return 10; }
        }

        /// <summary>
        /// Проверяет, что тип является структурой.
        /// </summary>
        public bool Accept(Type objectType) {
            return objectType.IsValueType && Type.GetTypeCode(objectType) == TypeCode.Object;
        }

        /// <summary>
        /// Получает класс информации о структуре.
        /// </summary>
        public Type GetTypeInfo(Type objectType) {
            return typeof(StructTypeInfo<>).MakeGenericType(objectType);
        }
    }
}
