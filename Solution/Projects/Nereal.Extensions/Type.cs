/*
 *  $Id: Type.cs 192 2010-12-03 13:04:04Z thenn $
 *  This file is a part of Nereal Extensions library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nereal.Extensions {
    /// <summary>
    /// Расширения для класса Assembly.
    /// </summary>
    public static class AssemblyExtensions {
        /// <summary>
        /// Получает из сборки набор типов, являющихся не-абстрактными потомками указанного типа.
        /// </summary>
        public static IEnumerable<Type> GetChildTypes(this Assembly asm, Type baseType) {
            foreach (var type in asm.GetTypes())
                if (!type.IsAbstract && type.IsSubclassOrImplements(baseType))
                    yield return type;
        }

        /// <summary>
        /// Получает из сборки набор пар атрибут/тип, являющихся потомками указанного типа и имеющих указанный атрибут.
        /// </summary>
        public static IEnumerable<Tuple<T, Type>> GetChildTypes<T>(this Assembly asm, Type baseType) where T : Attribute {
            foreach (var type in asm.GetChildTypes(baseType)) {
                var attr = type.GetAttribute<T>();
                if (attr != null)
                    yield return Tuple.Create(attr, type);
            }
        }
    }

    /// <summary>
    /// Расширения для класса Type.
    /// </summary>
    public static class TypeExtensions {
        /// <summary>
        /// Проверяет, что тип является производным от указанного генерик-типа.
        /// </summary>
        public static bool IsGenericFrom(this Type type, Type genericType) {
            return type.IsGenericType && type.GetGenericTypeDefinition() == genericType;
        }

        /// <summary>
        /// Проверяет, что тип реализует указанный интерфейс.
        /// </summary>
        public static bool Implements(this Type type, Type interfaceType) {
            return interfaceType.IsInterface && interfaceType.IsAssignableFrom(type);
        }

        /// <summary>
        /// Проверяет, является ли тип подклассом указанного типа (или его реализацией, если это интерфейс).
        /// </summary>
        public static bool IsSubclassOrImplements(this Type type, Type baseType) {
            return baseType.IsInterface ? baseType.IsAssignableFrom(type) : type.IsSubclassOf(baseType);
        }
    }

    /// <summary>
    /// Расширения для класса MemberInfo (включая Type, FieldInfo, PropertyInfo, и другие).
    /// </summary>
    public static class MemberInfoExtensions {
        /// <summary>
        /// Проверяет наличие атрибута указанного типа.
        /// </summary>
        public static bool HasAttribute<T>(this MemberInfo member) where T : Attribute {
            return member.HasAttribute<T>(false);
        }

        /// <summary>
        /// Проверяет наличие атрибута указанного типа. Если указано, то включая унаследованные атрибуты.
        /// </summary>
        public static bool HasAttribute<T>(this MemberInfo member, bool inherit) where T : Attribute {
            return member.GetAttribute<T>(inherit) != null;
        }

        /// <summary>
        /// Получает атрибут указанного типа. При отсутствии атрибута возвращает null.
        /// </summary>
        public static T GetAttribute<T>(this MemberInfo member) where T : Attribute {
            return member.GetAttribute<T>(false);
        }

        /// <summary>
        /// Получает атрибут указанного типа. Если указано, то включая унаследованные атрибуты. При отсутствии атрибута возвращает null.
        /// </summary>
        public static T GetAttribute<T>(this MemberInfo member, bool inherit) where T : Attribute {
            var attr = Attribute.GetCustomAttribute(member, typeof(T), inherit);
            return attr as T;
        }

        /// <summary>
        /// Получает набор атрибутов указанного типа.
        /// </summary>
        public static IEnumerable<T> GetAttributes<T>(this MemberInfo member) where T : Attribute {
            return member.GetAttributes<T>(false);
        }

        /// <summary>
        /// Получает набор атрибутов указанного типа. Если указано, то включая унаследованные атрибуты.
        /// </summary>
        public static IEnumerable<T> GetAttributes<T>(this MemberInfo member, bool inherit) where T : Attribute {
            foreach (var attr in Attribute.GetCustomAttributes(member, typeof(T), inherit))
                if (attr != null && attr is T)
                    yield return (T) attr;
        }

        /// <summary>
        /// Получает значение из атрибута указанного типа. При отсутствии атрибута возвращает указанное значение по умолчанию.
        /// </summary>
        public static V GetAttributeValue<T, V>(this MemberInfo member, bool inherit, Func<T, V> getter, V defaultValue) where T : Attribute {
            var attr = member.GetAttribute<T>(inherit);
            return attr != null ? getter(attr) : defaultValue;
        }
    }

    /// <summary>
    /// Расширения для MethodInfo.
    /// </summary>
    public static class MethodInfoExtensions {
        /// <summary>
        /// Получает сигнатуру метода в виде строки.
        /// </summary>
        public static string GetSignature(this MethodInfo method) {
            var sb = new StringBuilder();
            sb.Append(method.ReturnType.Name).Append(' ').Append(method.Name);
            if (method.IsGenericMethod)
                sb.Append('<').Append(method.GetGenericArguments(), t => t.FullName, ", ").Append('>');
            sb.Append('(').Append(method.GetParameters(), BuildParameter, ", ").Append(')');
            return sb.ToString();
        }

        /// <summary>
        /// Добавляет в StringBuilder строковое представление параметра метода.
        /// </summary>
        private static void BuildParameter(StringBuilder builder, ParameterInfo parameter) {
            builder.Append(parameter.ParameterType.Name).Append(' ').Append(parameter.Name);
        }
    }
}

