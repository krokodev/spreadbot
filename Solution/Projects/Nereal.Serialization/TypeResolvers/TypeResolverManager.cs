/*
 *  $Id: TypeResolverManager.cs 184 2010-11-28 12:51:22Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Reflection;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Менеджер определителей типов.
    /// </summary>
    public sealed class TypeResolverManager : CacheDictionary<Type, ITypeResolver> {
        /// <summary>
        /// Стандартный определитель типов по умолчанию.
        /// </summary>
        private static readonly ITypeResolver DefaultResolver = new FullNameTypeResolver();

        private ITypeResolver _default;

        /// <summary>
        /// Определитель типов по умолчанию.
        /// </summary>
        public ITypeResolver Default {
            get { return _default ?? DefaultResolver; }
            set { _default = value; }
        }

        /// <summary>
        /// Находит и подготовляет определитель типов для указанного типа.
        /// </summary>
        public void Prepare(Type type) {
            GetItem(type);
        }

        /// <summary>
        /// Получает словарь идентификаторов типов для указанного корневого типа.
        /// </summary>
        public ITypeIdDictionary GetTypeIdDictionary(Type rootType) {
            return GetItem(rootType) as ITypeIdDictionary;
        }

        /// <summary>
        /// Создает новый определитель типов при отсутствии такового на указанный тип.
        /// </summary>
        protected override ITypeResolver CreateItem(Type key) {
            return GetAttrResolver(key) ?? Default;
        }

        /// <summary>
        /// Находит или создает определитель типов по атрибуту <see cref="TypeResolverAttribute"/> у указанного типа.
        /// Возвращает null, если атрибут не найден ни у указанного типа, ни у одного из его предков.
        /// </summary>
        private ITypeResolver GetAttrResolver(Type objectType) {
            var attr = objectType.GetAttribute<TypeResolverAttribute>();
            if (attr == null) {
                if (objectType.BaseType == null)
                    return null;
                return GetItem(objectType.BaseType);
            } else {
                var resolver = CreateResolver(attr);
                if (resolver == null)
                    throw SerializationException.NotImplements(attr.ResolverType, typeof(ITypeResolver));
                return resolver;
            }
        }

        /// <summary>
        /// Создает новый определитель типов по информации в указанном атрибуте.
        /// </summary>
        private static ITypeResolver CreateResolver(TypeResolverAttribute attr) {
            var ci = attr.ResolverType.GetConstructor(new Type[] { typeof(string) });
            if (ci != null)
                return Activator.CreateInstance(attr.ResolverType, attr.XmlAttributeName) as ITypeResolver;
            ci = attr.ResolverType.GetConstructor(Type.EmptyTypes);
            if (ci != null)
                return Activator.CreateInstance(attr.ResolverType) as ITypeResolver;
            throw new SerializationException(string.Format("Type '{0}' doesn't have a constructor with string parameter or without parameters.", attr.ResolverType));
        }
    }
}

