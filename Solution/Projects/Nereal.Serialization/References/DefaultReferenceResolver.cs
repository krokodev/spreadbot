/*
 *  $Id: DefaultReferenceResolver.cs 182 2010-11-28 12:24:42Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Абстрактный класс стандартной реализации регистрации коллектора ссылок.
    /// </summary>
    internal abstract class DefaultReferenceResolver<T> {
        private string _collectorName;

        protected DefaultReferenceResolver(string collectorName) {
            _collectorName = collectorName;
        }

        /// <summary>
        /// Имя коллектора ссылок.
        /// </summary>
        public string CollectorName {
            get { return _collectorName; }
        }

        /// <summary>
        /// Выполняет действия перед десериализацией.
        /// </summary>
        public abstract void OnBeforeDeserialize(DeserializationContext context, T resolver);
        /// <summary>
        /// Выполняет действия после десериализации.
        /// </summary>
        public abstract void OnAfterDeserialize(DeserializationContext context, T resolver);
        /// <summary>
        /// Выполняет действия перед сериализацией.
        /// </summary>
        public abstract void OnBeforeSerialize(SerializationContext context, T resolver);
        /// <summary>
        /// Выполняет действия после сериализации.
        /// </summary>
        public abstract void OnAfterSerialize(SerializationContext context, T resolver);

        /// <summary>
        /// Создает все стандартные регистраторы коллекторов по всем атрибутам <see cref="ReferenceResolverAttribute" /> у данного типа.
        /// </summary>
        public static IEnumerable<DefaultReferenceResolver<T>> Create() {
            var objectType = typeof(T);
            var interfaceType = typeof(IKeyResolver<, >);
            var resolverGenericType = typeof(DefaultReferenceResolver<, , >);
            foreach (var attr in objectType.GetAttributes<ReferenceResolverAttribute>(true)) {
                var resolverInterface = interfaceType.MakeGenericType(attr.KeyType, attr.ValueType);
                if (!objectType.Implements(resolverInterface))
                    throw SerializationException.NotImplements(objectType, resolverInterface);
                var resolverType = resolverGenericType.MakeGenericType(objectType, attr.KeyType, attr.ValueType);
                var resolver = (DefaultReferenceResolver<T>) Activator.CreateInstance(resolverType, attr.CollectorName);
                yield return resolver;
            }
        }
    }

    /// <summary>
    /// Основная реализация регистрации коллекторов ссылок.
    /// </summary>
    internal sealed class DefaultReferenceResolver<T, TKey, TValue> : DefaultReferenceResolver<T> where T : IKeyResolver<TKey, TValue> {
        public DefaultReferenceResolver(string collectorName) : base(collectorName) {
        }

        /// <summary>
        /// Регистрирует коллектор перед десериализацией.
        /// </summary>
        public override void OnBeforeDeserialize(DeserializationContext context, T resolver) {
            context.Serializer.Collectors.Add(CollectorName, new ReferenceCollector<TKey, TValue>(resolver));
        }

        /// <summary>
        /// Вызывает определение ссылок и удаляет коллектор после десериализации.
        /// </summary>
        public override void OnAfterDeserialize(DeserializationContext context, T resolver) {
            context.Serializer.Collectors.ResolveAndRemove(CollectorName);
        }

        /// <summary>
        /// Регистрирует коллектор перед сериализацией.
        /// </summary>
        public override void OnBeforeSerialize(SerializationContext context, T resolver) {
            context.Serializer.Collectors.Add(CollectorName, new ReferenceCollector<TKey, TValue>(resolver));
        }

        /// <summary>
        /// Удаляет коллектор после десериализации.
        /// </summary>
        public override void OnAfterSerialize(SerializationContext context, T resolver) {
            context.Serializer.Collectors.Remove(CollectorName);
        }
    }
}
