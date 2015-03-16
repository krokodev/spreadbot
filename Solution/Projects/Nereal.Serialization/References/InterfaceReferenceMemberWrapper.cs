/*
 *  $Id: InterfaceReferenceMemberWrapper.cs 195 2010-12-05 12:26:31Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Обертка над полем или свойством, являющимся интерфейсной ссылкой.
    /// </summary>
    public sealed class InterfaceReferenceMemberWrapper<T, V> : ValueMemberWrapper<T, V> {
        private const string ImplementationAttributeName = "impl";

        private List<InterfaceReferenceVariant> _variants;

        /// <summary>
        /// Создает новый экземпляр обертки над интерфейсным ссылочными полем или свойством.
        /// Выдает исключение, если поле/свойство является параметром конструктора или read-only (они не могут быть ссылками).
        /// </summary>
        public InterfaceReferenceMemberWrapper(MemberInfoEx member) : base(member) {
            _variants = new List<InterfaceReferenceVariant>();
            foreach (var attr in member.MemberInfo.GetAttributes<InterfaceReferenceAttribute>()) {
                if (FindVariant(attr.BaseType, false) != null || FindVariant(attr.ImplementationName) != null)
                    throw new SerializationException("Duplicate base type or implementation name in InterfaceReferenceAttribute at member {0}.", member.MemberInfo);
                var ex = SerializationException.TestReferenceMember(this, attr.External);
                if (ex != null)
                    throw ex;
                _variants.Add(new InterfaceReferenceVariant(this, attr));
            }
            if (_variants.Count == 0)
                throw new SerializationException("InterfaceReferenceMemberWrapper cannot be created for member without InterfaceReferenceAttribute.");
        }

        /// <summary>
        /// Ищет вариант интерфейсной ссылки по типу.
        /// </summary>
        private InterfaceReferenceVariant FindVariant(Type type, bool inherit) {
            for (int i = 0; i < _variants.Count; i++) {
                var v = _variants[i];
                if (v.BaseType == type || (inherit && type.IsSubclassOf(v.BaseType)))
                    return v;
            }
            return null;
        }
        /// <summary>
        /// Ищет вариант интерфейсной ссылки по имени.
        /// </summary>
        private InterfaceReferenceVariant FindVariant(string implName) {
            for (int i = 0; i < _variants.Count; i++) {
                var v = _variants[i];
                if (v.ImplementationName == implName)
                    return v;
            }
            return null;
        }

        /// <summary>
        /// Интерфейсные ссылки всегда сохраняются в виде элементов.
        /// </summary>
        public override bool IsElement {
            get { return true; }
        }

        /// <summary>
        /// Клонирует указанное значение как ссылку, если на данный тип есть вариант. Иначе обычное глубокое клонирование.
        /// </summary>
        protected override V CloneValue(V value) {
            if (ValueInfo.IsNull(value))
                return value;
            var variant = FindVariant(value.GetType(), true);
            return variant != null ? value : base.CloneValue(value);
        }

        /// <summary>
        /// Сравнивает два значения как ссылки, если на данный тип есть вариант. Иначе обычное глубокое сравнение.
        /// </summary>
        protected override bool EqualsValue(V a, V b) {
            bool nullA = ValueInfo.IsNull(a), nullB = ValueInfo.IsNull(b);
            if (nullA || nullB)
                return nullA && nullB;
            var type = a.GetType();
            if (type != b.GetType())
                return false;
            var variant = FindVariant(type, true);
            if (variant == null)
                return ValueInfo.GetAdapter(type).Equals(a, b);
            if (MemberType.IsValueType)
                return ValueInfo.Equals(a, b);
            else
                return object.ReferenceEquals(a, b);
        }

        /// <summary>
        /// Десериализует поле/свойство и возвращает его в виде обертки над значением.
        /// </summary>
        public override MemberValue<T> DeserializeMemberValue(DeserializationContext context) {
            var variant = ReadVariant(context);
            if (variant != null)
                return variant.Serializer.DeserializeMemberValue<T>(context, this);
            return base.DeserializeMemberValue(context);
        }

        /// <summary>
        /// Десериализует поле/свойство и добавляет его в коллектор ссылок.
        /// </summary>
        public override void Deserialize(DeserializationContext context, T obj) {
            V value;
            if (DeserializeReference(context, obj, out value))
                SetValue(obj, value);
        }
        /// <summary>
        /// Десериализует поле/свойство и добавляет его в коллектор ссылок, с передачей объекта по ссылке (для структур).
        /// </summary>
        public override void Deserialize(DeserializationContext context, ref T obj) {
            V value;
            if (DeserializeReference(context, obj, out value))
                SetValue(ref obj, value);
        }

        /// <summary>
        /// Десериализует ссылку, и возвращает true, если значение уже получено и готово к установке.
        /// </summary>
        private bool DeserializeReference(DeserializationContext context, T obj, out V value) {
            var variant = ReadVariant(context);
            if (variant == null || variant.Serializer.External)
                return DeserializeValue(context, out value);
            value = DefaultValue;
            return !variant.Serializer.DeserializeReference(context, v => SetValue(obj, v));
        }

        /// <summary>
        /// Десериализует значение поля/свойства и возвращает его.
        /// </summary>
        protected override bool DeserializeValue(DeserializationContext context, out V value) {
            var variant = ReadVariant(context);
            if (variant != null)
                return variant.Serializer.DeserializeValue(context, DefaultValue, out value);
            return base.DeserializeValue(context, out value);
        }

        /// <summary>
        /// Сериализует значение поля/свойства.
        /// </summary>
        protected override void SerializeValue(SerializationContext context, V value) {
            if (!ValueInfo.IsNull(value)) {
                var variant = FindVariant(value.GetType(), true);
                if (variant != null) {
                    context.StartElement(Name);
                    context.WriteAttribute(ImplementationAttributeName, variant.ImplementationName);
                    variant.Serializer.SerializeValue(context, value);
                    context.EndElement();
                    return;
                }
            }
            context.Write(this, value);
        }

        /// <summary>
        /// Десериализует имя варианта интерфейсной ссылки и ищет по нему вариант.
        /// </summary>
        private InterfaceReferenceVariant ReadVariant(DeserializationContext context) {
            var implName = context.ReadAttribute(ImplementationAttributeName, null);
            if (string.IsNullOrEmpty(implName))
                return null;
            return FindVariant(implName);
        }

        private class InterfaceReferenceVariant {
            public readonly Type BaseType;
            public readonly string ImplementationName;
            public readonly ReferenceSerializer<V> Serializer;

            public InterfaceReferenceVariant(MemberWrapper member, InterfaceReferenceAttribute attr) {
                BaseType = attr.BaseType;
                ImplementationName = attr.ImplementationName;
                Serializer = ReferenceSerializer<V>.Create(member, attr, BaseType);
            }
        }
    }
}