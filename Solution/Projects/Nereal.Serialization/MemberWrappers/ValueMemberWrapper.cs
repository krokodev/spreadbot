/*
 *  $Id: ValueMemberWrapper.cs 194 2010-12-05 12:03:19Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Reflection;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Обертка над полем или свойством с указанными типами владельца и значения.
    /// </summary>
    public class ValueMemberWrapper<T, V> : TypeMemberWrapper<T> {
        private TypeInfo<V> _valueInfo;
        private MemberAccessors<T, V> _accessors;
        private bool _hasDefaultValue;
        private V _defaultValue;

        /// <summary>
        /// Создает новый экземпляр обертки над полем/свойством с учетом типа значения.
        /// </summary>
        public ValueMemberWrapper(MemberInfoEx member) : base(member) {
            _accessors = member.GetAccessors<T, V>(SerializationConfig.AccessorFactory);
            _valueInfo = SerializationConfig.InfoManager.GetInfo<V>();
            UpdateDefaultValue();
            _valueInfo.TestMember(this);
        }

        /// <summary>
        /// Информация о типе значения.
        /// </summary>
        protected TypeInfo<V> ValueInfo {
            get { return _valueInfo; }
        }

        /// <summary>
        /// Получает значение поля/свойства.
        /// </summary>
        public V GetValue(T obj) {
            var getter = _accessors.Getter;
            return getter != null ? getter(obj) : DefaultValue;
        }
        /// <summary>
        /// Получает значение поля/свойства, с передачей объекта по ссылке (для структур).
        /// </summary>
        public V GetValue(ref T obj) {
            var getter = _accessors.RefGetter;
            return getter != null ? getter(ref obj) : DefaultValue;
        }

        /// <summary>
        /// Устанавливает значение поля/свойства.
        /// </summary>
        public void SetValue(T obj, V value) {
            var setter = _accessors.Setter;
            if (setter != null)
                setter(obj, value);
        }
        /// <summary>
        /// Устанавливает значение поля/свойства, с передачей объекта по ссылке (для структур).
        /// </summary>
        public void SetValue(ref T obj, V value) {
            var setter = _accessors.RefSetter;
            if (setter != null)
                setter(ref obj, value);
        }

        /// <summary>
        /// Является ли поле/свойство xml-элементом, с учетом типа значения.
        /// </summary>
        public override bool IsElement {
            get { return base.IsElement || _valueInfo.IsElement(this); }
        }

        /// <summary>
        /// Имеет ли поле/свойство значение по умолчанию.
        /// </summary>
        public bool HasDefaultValue {
            get { return _hasDefaultValue; }
        }

        /// <summary>
        /// Значение по умолчанию.
        /// </summary>
        public V DefaultValue {
            get { return _defaultValue; }
        }

        /// <summary>
        /// Получает значение поля/свойства в виде object.
        /// </summary>
        public override object GetObjectValue(T obj) {
            return GetValue(obj);
        }
        /// <summary>
        /// Получает значение поля/свойства в виде object, с передачей объекта по ссылке (для структур).
        /// </summary>
        public override object GetObjectValue(ref T obj) {
            return GetValue(ref obj);
        }

        /// <summary>
        /// Получает копию значения поля/свойства в виде object.
        /// </summary>
        public override object CloneObjectValue(T obj) {
            return Clone(obj);
        }
        /// <summary>
        /// Получает копию значения поля/свойства в виде object, с передачей объекта по ссылке (для структур).
        /// </summary>
        public override object CloneObjectValue(ref T obj) {
            return Clone(ref obj);
        }

        /// <summary>
        /// Клонирует значение поля/свойства.
        /// </summary>
        public V Clone(T obj) {
            return CloneValue(GetValue(obj));
        }
        /// <summary>
        /// Клонирует значение поля/свойства, с передачей объекта по ссылке (для структур).
        /// </summary>
        public V Clone(ref T obj) {
            return CloneValue(GetValue(ref obj));
        }

        /// <summary>
        /// Клонирует указанное значение.
        /// </summary>
        protected virtual V CloneValue(V value) {
            if (_valueInfo.IsNull(value))
                return value;
            var info = value.GetType().GetInfo<V>();
            return info.Clone(value);
        }

        /// <summary>
        /// Сравнивает поле/свойство из двух объектов.
        /// </summary>
        public override bool Equals(T a, T b) {
            return EqualsValue(GetValue(a),  GetValue(b));
        }
        /// <summary>
        /// Сравнивает поле/свойство из двух объектов, с передачей объектов по ссылке (для структур).
        /// </summary>
        public override bool Equals(ref T a, ref T b) {
            return EqualsValue(GetValue(ref a), GetValue(ref b));
        }

        /// <summary>
        /// Сравнивает два значения.
        /// </summary>
        protected virtual bool EqualsValue(V a, V b) {
            return a.DeepEquals(b, _valueInfo);
        }

        /// <summary>
        /// Копирует поле/свойство из одного объекта в другой.
        /// </summary>
        public override void Copy(T src, T dest) {
            SetValue(dest, Clone(src));
        }
        /// <summary>
        /// Копирует поле/свойство из одного объекта в другой, с передачей объекта по ссылке (для структур).
        /// </summary>
        public override void Copy(ref T src, ref T dest) {
            SetValue(ref dest, Clone(ref src));
        }

        /// <summary>
        /// Десериализует поле/свойство и возвращает его в виде обертки над значением.
        /// </summary>
        public override MemberValue<T> DeserializeMemberValue(DeserializationContext context) {
            V value;
            var exists = DeserializeValue(context, out value);
            return new MemberValue<T, V>(this, exists, value);
        }

        /// <summary>
        /// Десериализует поле/свойство и устанавливает его в объект.
        /// </summary>
        public override void Deserialize(DeserializationContext context, T obj) {
            if (IsReadOnly) {
                var value = GetValue(obj);
                DeserializeValue(context, value);
            } else {
                V value;
                if (DeserializeValue(context, out value))
                    SetValue(obj, value);
            }
        }
        /// <summary>
        /// Десериализует поле/свойство и устанавливает его в объект, с передачей объекта по ссылке (для структур).
        /// </summary>
        public override void Deserialize(DeserializationContext context, ref T obj) {
            if (IsReadOnly) {
                var value = GetValue(ref obj);
                DeserializeValue(context, value);
            } else {
                V value;
                if (DeserializeValue(context, out value))
                    SetValue(ref obj, value);
            }
        }

        /// <summary>
        /// Сериализует поле/свойство.
        /// </summary>
        public override void Serialize(SerializationContext context, T obj) {
            var value = GetValue(obj);
            SerializeValue(context, value);
        }
        /// <summary>
        /// Сериализует поле/свойство, с передачей объекта по ссылке (для структур).
        /// </summary>
        public override void Serialize(SerializationContext context, ref T obj) {
            var value = GetValue(ref obj);
            SerializeValue(context, value);
        }

        /// <summary>
        /// Десериализует значение поля/свойства и возвращает его.
        /// </summary>
        protected virtual bool DeserializeValue(DeserializationContext context, out V value) {
            if (IsElement) {
                value = context.ReadElement<V>(this);
                return true;
            } else {
                var exists = context.HasAttribute(Name);
                value = exists ? _valueInfo.ConvertFromString(context.ReadAttribute(Name)) : DefaultValue;
                return exists || HasDefaultValue;
            }
        }

        /// <summary>
        /// Десериализует существующее значение поля/свойства, обновляя его.
        /// </summary>
        private void DeserializeValue(DeserializationContext context, V value) {
            if (_valueInfo.IsNull(value))
                return;
            if (IsElement)
                context.ReadElement(this, value);
            else if (context.HasAttribute(Name))
                _valueInfo.ConvertFromString(context.ReadAttribute(Name), value);
        }

        /// <summary>
        /// Сериализует значение поля/свойства.
        /// </summary>
        protected virtual void SerializeValue(SerializationContext context, V value) {
            if (NeedSerialize(value))
                context.Write(this, value);
        }

        /// <summary>
        /// Проверяет, нужно ли сериализовать указанное значение.
        /// </summary>
        protected bool NeedSerialize(V value) {
            return IsElement || _valueInfo.NeedSerialize(value, _hasDefaultValue, _defaultValue);
        }

        /// <summary>
        /// Обновляет свойства значения по умолчанию.
        /// </summary>
        private void UpdateDefaultValue() {
            var isNullable = typeof(V).IsGenericFrom(typeof(Nullable<>));
            var attr = MemberInfo.GetAttribute<DefaultValueAttribute>(true);
            if (attr != null) {
                if (isNullable)
                    throw new SerializationException(string.Format("Member {0} is Nullable and cannot have default value.", this));
                _hasDefaultValue = true;
                try {
                    if (attr.DefaultValue is string)
                        _defaultValue = _valueInfo.ConvertFromString((string) attr.DefaultValue);
                    else
                        _defaultValue = (V) Convert.ChangeType(attr.DefaultValue, typeof(V));
                } catch (Exception e) {
                    throw new SerializationException(string.Format("Default value for member {0} cannot be converted to a '{1}' type.", this, typeof(V)), e);
                }
            } else {
                _hasDefaultValue = isNullable;
                _defaultValue = default(V);
            }
        }
    }
}
