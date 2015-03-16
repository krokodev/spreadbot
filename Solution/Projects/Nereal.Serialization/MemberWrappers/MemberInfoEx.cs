/*
 *  $Id: MemberInfoEx.cs 188 2010-11-29 16:20:38Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nereal.Serialization {
    /// <summary>
    /// Абстрактный класс расширенной информации о члене типа (поле или свойстве).
    /// Для унификации работы с полями и свойствами.
    /// </summary>
    public abstract class MemberInfoEx {
        /// <summary>
        /// Стандартная информация о поле или свойстве.
        /// </summary>
        public abstract MemberInfo MemberInfo { get; }
        /// <summary>
        /// Тип поля или свойства.
        /// </summary>
        public abstract Type MemberType { get; }
        /// <summary>
        /// Является ли поле или свойство только для чтения.
        /// </summary>
        public abstract bool IsReadOnly { get; }
        /// <summary>
        /// Получает набор делегатов доступа к данному полю или свойству.
        /// </summary>
        public abstract MemberAccessors<T, V> GetAccessors<T, V>(IAccessorFactory factory);
    }

    /// <summary>
    /// Расширения для MemberInfoEx.
    /// </summary>
    public static class MemberInfoExExtensions {
        /// <summary>
        /// Получает расширенную информацию обо всех полях и свойствах типа.
        /// </summary>
        public static IEnumerable<MemberInfoEx> GetMembersEx(this Type type) {
            var members = new List<MemberInfoEx>();
            type.GetMembersEx(members);
            return members;
        }

        /// <summary>
        /// Получает расширенную информацию обо всех полях и свойствах типа, добавляя её в указанный список.
        /// </summary>
        private static void GetMembersEx(this Type type, List<MemberInfoEx> members) {
            if (type.BaseType != null && type.BaseType != typeof(object))
                type.BaseType.GetMembersEx(members);
            foreach (var f in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                if (FieldInfoEx.IsSerializable(f))
                    members.Add(new FieldInfoEx(f));
            foreach (var p in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                if (PropertyInfoEx.IsSerializable(p))
                    AddMember(members, new PropertyInfoEx(p));
        }

        /// <summary>
        /// Добавляет информацию о свойстве в список или обновляет имеющуюся, если таковая уже есть в списке.
        /// </summary>
        private static void AddMember(List<MemberInfoEx> members, PropertyInfoEx member) {
            if (!member.PropertyInfo.GetGetMethod(true).IsPrivate) {
                for (int i = 0; i < members.Count; i++) {
                    if (members[i].MemberInfo.Name == member.MemberInfo.Name) {
                        members[i] = member;
                        return;
                    }
                }
            }
            members.Add(member);
        }
    }
}
