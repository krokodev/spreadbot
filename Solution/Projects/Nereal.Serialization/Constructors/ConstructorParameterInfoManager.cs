/*
 *  $Id: ConstructorParameterInfoManager.cs 109 2010-10-22 15:17:29Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Linq.Expressions;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Менеджер информации о параметрах конструктора.
    /// </summary>
    public sealed class ConstructorParameterInfoManager : CacheDictionary<Type, ConstructorParameterInfo> {
        /// <summary>
        /// Регистрирует информацию о параметрах конструктора по списку имен полей и свойств.
        /// </summary>
        public void Register<T>(params string[] names) {
            var type = typeof(T);
            Add(type, ConstructorParameterInfo.Create(type, names));
        }

        /// <summary>
        /// Регистрирует информацию о параметрах конструктора по списку Expression, содержащих доступ к полям и свойствам.
        /// </summary>
        public void Register<T>(params Expression<Func<T, object>>[] members) {
            var type = typeof(T);
            Add(type, ConstructorParameterInfo.Create(members));
        }

        /// <summary>
        /// Ищем и создает информацию о параметрах конструктора по ранее не зарегистрированному типу.
        /// </summary>
        protected override ConstructorParameterInfo CreateItem(Type key) {
            var attr = key.GetAttribute<ConstructorParametersAttribute>();
            if (attr != null)
                return ConstructorParameterInfo.Create(key, attr.ParameterMemberNames);
            else
                return ConstructorParameterInfo.Empty;
        }
    }
}
