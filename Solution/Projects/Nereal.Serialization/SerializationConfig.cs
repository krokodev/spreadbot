/*
 *  $Id: SerializationConfig.cs 198 2010-12-07 13:43:37Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nereal.Serialization {
    /// <summary>
    /// Доступ к настройкам и данным сериализации.
    /// </summary>
    public static class SerializationConfig {
        /// <summary>
        /// Фабрика делегатов доступа.
        /// </summary>
        public static readonly IAccessorFactory AccessorFactory;

        /// <summary>
        /// Список сборок, игнорируемых при сканировании типов.
        /// </summary>
        public static readonly List<string> IgnoreScanAssemblies;
        /// <summary>
        /// Менеджер определителей типов.
        /// </summary>
        public static readonly TypeResolverManager ResolverManager;
        /// <summary>
        /// Менеджер информации о параметрах конструкторов.
        /// </summary>
        public static readonly ConstructorParameterInfoManager ConstructorManager;
        /// <summary>
        /// Менеджер информации о типах.
        /// </summary>
        public static readonly TypeInfoManager InfoManager;

        static SerializationConfig() {
            AccessorFactory = new DynamicAccessorFactory();
            IgnoreScanAssemblies = new List<string> { "mscorlib", "System", "System.Core", "System.Xml" };
            ResolverManager = new TypeResolverManager();
            ConstructorManager = new ConstructorParameterInfoManager();
            InfoManager = new TypeInfoManager();
        }

        /// <summary>
        /// Проверяет, должна ли указанная сборка игнорироваться при сканировании типов.
        /// </summary>
        public static bool IsIgnoreScanAssembly(Assembly assembly) {
            return IgnoreScanAssemblies.Contains(assembly.GetName().Name);
        }

        /// <summary>
        /// Получает информацию о типе.
        /// </summary>
        public static AbstractTypeInfo GetInfo(this Type type) {
            return InfoManager[type];
        }
        /// <summary>
        /// Получает адаптированную информацию о типе.
        /// </summary>
        public static TypeInfo<T> GetInfo<T>(this Type type) {
            return InfoManager.GetAdaptedInfo<T>(type);
        }

        /// <summary>
        /// Получает определитель типов.
        /// </summary>
        public static ITypeResolver GetResolver(this Type type) {
            return ResolverManager[type];
        }
    }
}

