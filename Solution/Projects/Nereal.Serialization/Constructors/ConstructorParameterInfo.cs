/*
 *  $Id: ConstructorParameterInfo.cs 189 2010-11-29 16:41:54Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Nereal.Serialization {
    /// <summary>
    /// Информация о параметрах конструктора.
    /// </summary>
    public sealed class ConstructorParameterInfo {
        private List<MemberInfo> _members;

        private ConstructorParameterInfo(IEnumerable<MemberInfo> members) {
            _members = new List<MemberInfo>();
            if (members != null)
                _members.AddRange(members);
        }

        /// <summary>
        /// Количество параметров.
        /// </summary>
        public int Count {
            get { return _members.Count; }
        }

        /// <summary>
        /// Поля и свойства, связанные с соответствующим номером параметра.
        /// </summary>
        /// <param name="index">
        /// A <see cref="System.Int32"/>
        /// </param>
        public MemberInfo this[int index] {
            get { return _members[index]; }
        }

        /// <summary>
        /// Проверяет наличие указанного поля/свойства в списке параметров.
        /// </summary>
        public bool Contains(MemberInfo member) {
            return _members.Exists(m => MemberEquals(m, member));
        }

        /// <summary>
        /// Ищет номер параметра по соответствующему полю или свойству. Возвращает -1, если не найдено.
        /// </summary>
        public int Find(MemberInfo member) {
            return _members.FindLastIndex(m => MemberEquals(m, member));
        }

        /// <summary>
        /// Сравнивает два MemberInfo на идентичность.
        /// </summary>
        private static bool MemberEquals(MemberInfo a, MemberInfo b) {
            return a.MemberType == b.MemberType && a.Name == b.Name && a.DeclaringType == b.DeclaringType;
        }

        /// <summary>
        /// Статический экземпляр, представляющий пустой список параметров.
        /// </summary>
        public static readonly ConstructorParameterInfo Empty = new ConstructorParameterInfo(null);

        /// <summary>
        /// Создает информацию о параметрах конструктора по типу и списку имен полей и свойств.
        /// </summary>
        public static ConstructorParameterInfo Create(Type type, IEnumerable<string> names) {
            return new ConstructorParameterInfo(GetMembers(type, names));
        }

        /// <summary>
        /// Создает информацию о параметрах конструктора по списку Expression, содержащих доступ к полям и свойствам.
        /// </summary>
        public static ConstructorParameterInfo Create<T>(IEnumerable<Expression<Func<T, object>>> members) {
            return new ConstructorParameterInfo(GetMembers<T>(members));
        }

        /// <summary>
        /// Получает список полей и свойств по типу и их именам.
        /// </summary>
        private static IEnumerable<MemberInfo> GetMembers(Type type, IEnumerable<string> names) {
            var members = new List<MemberInfoEx>(type.GetMembersEx());
            foreach (var name in names) {
                var member = members.FindLast(m => m.MemberInfo.Name == name);
                if (member != null)
                    yield return member.MemberInfo;
            }
        }

        /// <summary>
        /// Получает список полей и свойств по списку Expression, содержащих доступ к ним.
        /// </summary>
        private static IEnumerable<MemberInfo> GetMembers<T>(IEnumerable<Expression<Func<T, object>>> members) {
            var type = typeof(T);
            foreach (var expr in members) {
                if (expr.Body.NodeType != ExpressionType.MemberAccess)
                    throw new SerializationException(string.Format("Expression for constructor parameter of type '{0}' isn't a member access expression.", type));
                var memberExpr = expr.Body as MemberExpression;
                if (memberExpr.Expression.Type != type)
                    throw new SerializationException(string.Format("MemberExpression for constructor parameter of type '{0}' is a member of another type '{1}'.", type, memberExpr.Expression.Type));
                if (memberExpr.Member.MemberType != MemberTypes.Field && memberExpr.Member.MemberType != MemberTypes.Property)
                    throw new SerializationException(string.Format("Member '{1}' of type '{0}' isn't a field or a property.", type, memberExpr.Member.Name));
                yield return memberExpr.Member;
            }
        }
    }
}
