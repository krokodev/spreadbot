/*
 *  $Id: MemberWrapperFactory.cs 194 2010-12-05 12:03:19Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;
using System.Reflection;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Фабрика оберток над полями или свойствами.
    /// </summary>
    internal static class MemberWrapperFactory {
        /// <summary>
        /// Создает обертку по указанному полю/свойству.
        /// </summary>
        public static TypeMemberWrapper<T> Create<T>(MemberInfoEx member) {
            var wrapperType = GetWrapperType(typeof(T), member);
            return (TypeMemberWrapper<T>) Activator.CreateInstance(wrapperType, member);
        }

        /// <summary>
        /// Получает тип обертки по указанному полю/свойству.
        /// </summary>
        private static Type GetWrapperType(Type objectType, MemberInfoEx member) {
            // Проверка на InterfaceReferenceAttribute.
            if (member.MemberInfo.HasAttribute<InterfaceReferenceAttribute>()) {
                if (!member.MemberType.IsInterface)
                    throw new SerializationException("InterfaceReferenceAttribute cannot be created for non-interface-typed member {0}.", member.MemberInfo);
                return typeof(InterfaceReferenceMemberWrapper<, >).MakeGenericType(objectType, member.MemberType);
            }
            // Проверка на ReferenceAttribute.
            var keyType = member.MemberInfo.GetAttributeValue<ReferenceAttribute, Type>(false, attr => attr.KeyType, null);
            if (keyType != null) {
                if (member.MemberType.IsInterface)
                    throw new SerializationException("Interface-typed member {0} cannot be a reference.", member.MemberInfo);
                return typeof(ReferenceMemberWrapper<, , >).MakeGenericType(objectType, member.MemberType, keyType);
            }
            // Обычное поле/свойство.
            return typeof(ValueMemberWrapper<, >).MakeGenericType(objectType, member.MemberType);
        }

        /// <summary>
        /// Создает обертки по всем полям и свойствам указанного типа.
        /// </summary>
        public static IEnumerable<TypeMemberWrapper<T>> CreateAll<T>() {
            var objectType = typeof(T);
            var cpInfo = SerializationConfig.ConstructorManager[objectType];
            foreach (var member in objectType.GetMembersEx())
                if (!member.IsReadOnly || cpInfo.Contains(member.MemberInfo) || member.MemberType.GetInfo().IsUpdateable)
                    yield return Create<T>(member);
        }
    }
}
