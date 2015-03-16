/*
 *  $Id: ReferenceSerializer.cs 197 2010-12-07 11:46:25Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Внутренний абстрактный класс для сериализации ссылок. Не зависит от типов ключа и значения, только от типа поля/свойства.
    /// </summary>
    internal abstract class ReferenceSerializer<TMember> {
        private bool _external;

        public ReferenceSerializer(ReferenceAttribute attr) {
            _external = attr.External;
        }

        /// <summary>
        /// Является ли ссылка внешней или внутренней.
        /// </summary>
        public bool External {
            get { return _external; }
        }

        /// <summary>
        /// Сохраняется ли ссылка в виде элемента.
        /// </summary>
        public abstract bool IsElement { get; }

        /// <summary>
        /// Десериализует поле/свойство и возвращает его в виде обертки над значением.
        /// </summary>
        public abstract MemberValue<TOwner> DeserializeMemberValue<TOwner>(DeserializationContext context, ValueMemberWrapper<TOwner, TMember> member);
        /// <summary>
        /// Десериализует ключ, получает по нему значение, и возвращает его.
        /// </summary>
        public abstract bool DeserializeValue(DeserializationContext context, TMember defaultValue, out TMember value);
        /// <summary>
        /// Десериализует ссылку, добавляет её в коллектор, с последующей установкой с помощью указанного делегата.
        /// </summary>
        public abstract bool DeserializeReference(DeserializationContext context, Action<TMember> setter);
        /// <summary>
        /// Сериализует ключ указанного значения.
        /// </summary>
        public abstract void SerializeValue(SerializationContext context, TMember value);

        /// <summary>
        /// Создает новый сериализатор ссылок по указанным полю/свойству, атрибуту, и типу значения.
        /// </summary>
        public static ReferenceSerializer<TMember> Create(MemberWrapper member, ReferenceAttribute attr, Type valueType) {
            var type = typeof(ReferenceSerializer<, , >).MakeGenericType(typeof(TMember), attr.KeyType, valueType);
            return (ReferenceSerializer<TMember>) Activator.CreateInstance(type, member, attr);
        }
    }

    /// <summary>
    /// Внутренний класс для сериализации ссылок. Содержит все необходимые для этого свойства.
    /// </summary>
    internal sealed class ReferenceSerializer<TMember, TKey, TValue> : ReferenceSerializer<TMember> {
        private const string DefaultRefName = "ref";

        private TypeInfo<TKey> _keyInfo;
        private TypeInfo<TMember> _valueInfo;
        private MemberWrapper _keyMember;
        private string _resolverName;
        private bool _rootCollector;

        public ReferenceSerializer(MemberWrapper member, ReferenceAttribute attr) : base(attr) {
            _keyInfo = SerializationConfig.InfoManager.GetInfo<TKey>();
            _valueInfo = SerializationConfig.InfoManager.GetInfo<TMember>();
            var name = attr is InterfaceReferenceAttribute ? DefaultRefName : member.OriginalName;
            _keyMember = new MemberWrapper(typeof(TKey), false, name, MemberWrapper.DefaultItemName);
            _resolverName = attr.ResolverName;
            _rootCollector = attr.RootCollector;
        }

        /// <summary>
        /// Получает коллектор для данного поля/свойства.
        /// </summary>
        private ReferenceCollector<TKey, TValue> GetCollector(Serializer serializer) {
            return serializer.Collectors.Get<TKey, TValue>(_resolverName, _rootCollector);
        }

        /// <summary>
        /// Получает определитель ключей для данного поля/свойства.
        /// </summary>
        private IKeyResolver<TKey, TValue> GetResolver(Serializer serializer) {
            return External ? serializer.Resolvers.Get<TKey, TValue>(_resolverName) : GetCollector(serializer) as IKeyResolver<TKey, TValue>;
        }

        public override bool IsElement {
            get { return _keyInfo.IsElement(_keyMember); }
        }

        public override MemberValue<TOwner> DeserializeMemberValue<TOwner>(DeserializationContext context, ValueMemberWrapper<TOwner, TMember> member) {
            TKey key;
            DeserializeKey(context, out key);
            return new ReferenceMemberValue<TOwner, TMember, TKey, TValue>(member, GetCollector(context.Serializer), key);
        }

        public override bool DeserializeValue(DeserializationContext context, TMember defaultValue, out TMember value) {
            value = defaultValue;
            TKey key;
            if (!DeserializeKey(context, out key))
                return false;
            var resolver = GetResolver(context.Serializer);
            if (resolver == null)
                return false;
            value = (TMember) (object) resolver.ResolveValue(key);
            return true;
        }

        public override bool DeserializeReference(DeserializationContext context, Action<TMember> setter) {
            var collector = GetCollector(context.Serializer);
            if (collector == null)
                return false;
            TKey key;
            DeserializeKey(context, out key);
            var reference = new ReferenceWrapper<TKey, TValue>(v => setter((TMember) (object) v), key);
            collector.Add(reference);
            return true;
        }

        public override void SerializeValue(SerializationContext context, TMember value) {
            if (_valueInfo.IsNull(value) || !(value is TValue))
                return;
            var resolver = GetResolver(context.Serializer);
            if (resolver != null)
                SerializeKey(context, resolver.ResolveKey((TValue) (object) value));
        }

        /// <summary>
        /// Десериализует ключ и возвращает его.
        /// </summary>
        private bool DeserializeKey(DeserializationContext context, out TKey key) {
            if (IsElement) {
                key = context.ReadElement<TKey>(_keyMember);
                return true;
            } else {
                var exists = context.HasAttribute(_keyMember.Name);
                key = exists ? _keyInfo.ConvertFromString(context.ReadAttribute(_keyMember.Name)) : default(TKey);
                return exists;
            }
        }

        /// <summary>
        /// Сериализует ключ.
        /// </summary>
        private void SerializeKey(SerializationContext context, TKey key) {
            context.Write(_keyMember, key);
        }
    }
}
