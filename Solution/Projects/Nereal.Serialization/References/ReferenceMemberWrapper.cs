/*
 *  $Id: ReferenceMemberWrapper.cs 194 2010-12-05 12:03:19Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Reflection;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Обертка над полем или свойством, являющимся ссылкой.
    /// </summary>
    public class ReferenceMemberWrapper<T, V, K> : ValueMemberWrapper<T, V> {
        private ReferenceSerializer<V> _referenceSerializer;

        /// <summary>
        /// Создает новый экземпляр обертки над ссылочными полем или свойством.
        /// Выдает исключение, если поле/свойство является параметром конструктора или read-only (они не могут быть ссылками).
        /// </summary>
        public ReferenceMemberWrapper(MemberInfoEx member) : base(member) {
            var attr = MemberInfo.GetAttribute<ReferenceAttribute>(true);
            if (attr == null)
                throw new SerializationException("ReferenceMemberWrapper cannot be created for member without ReferenceAttribute.");
            var ex = SerializationException.TestReferenceMember(this, attr.External);
            if (ex != null)
                throw ex;
            _referenceSerializer = ReferenceSerializer<V>.Create(this, attr, member.MemberType);
        }

        /// <summary>
        /// Является ли поле/свойство xml-элементом, с учетом типа ключа.
        /// </summary>
        public override bool IsElement {
            get { return _referenceSerializer.IsElement; }
        }

        /// <summary>
        /// Клонирует указанное значение как ссылку, без глубокого клонирования.
        /// </summary>
        protected override V CloneValue(V value) {
            return value;
        }

        /// <summary>
        /// Сравнивает два значения как ссылки, без глубокого сравнения.
        /// </summary>
        protected override bool EqualsValue(V a, V b) {
            if (MemberType.IsValueType)
                return ValueInfo.Equals(a, b);
            else
                return object.ReferenceEquals(a, b);
        }

        /// <summary>
        /// Десериализует поле/свойство и возвращает его в виде обертки над значением.
        /// </summary>
        public override MemberValue<T> DeserializeMemberValue(DeserializationContext context) {
            if (_referenceSerializer.External)
                return base.DeserializeMemberValue(context);
            return _referenceSerializer.DeserializeMemberValue<T>(context, this);
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
            value = DefaultValue;
            return _referenceSerializer.External ? DeserializeValue(context, out value) : !_referenceSerializer.DeserializeReference(context, v => SetValue(obj, v));
        }

        /// <summary>
        /// Десериализует ключ, получает по нему значение, и возвращает его.
        /// </summary>
        protected override bool DeserializeValue(DeserializationContext context, out V value) {
            return _referenceSerializer.DeserializeValue(context, DefaultValue, out value);
        }

        /// <summary>
        /// Сериализует ключ указанного значения.
        /// </summary>
        protected override void SerializeValue(SerializationContext context, V value) {
            if (ValueInfo.IsNull(value) || !NeedSerialize(value))
                return;
            _referenceSerializer.SerializeValue(context, value);
        }
    }
}
