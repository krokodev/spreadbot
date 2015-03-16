/*
 *  $Id: ConstructorParametersAttribute.cs 144 2010-10-28 12:11:08Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Указывает список имен полей/свойств класса, которые при десериализации должны использоваться как параметры конструктора.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class ConstructorParametersAttribute : Attribute {
        /// <summary>
        /// Список имен полей/свойств, являющихся параметрами конструктора.
        /// </summary>
        public readonly string[] ParameterMemberNames;

        /// <summary>
        /// Создает новый экземпляр ConstructorParametersAttribute.
        /// </summary>
        public ConstructorParametersAttribute(params string[] parameterNames) {
            ParameterMemberNames = parameterNames;
        }
    }
}
