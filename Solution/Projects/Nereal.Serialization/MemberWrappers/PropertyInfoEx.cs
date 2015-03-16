/*
 *  $Id: PropertyInfoEx.cs 188 2010-11-29 16:20:38Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Reflection;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Расширенная информация о свойстве.
    /// </summary>
    internal sealed class PropertyInfoEx : MemberInfoEx {
        private PropertyInfo _property;

        public PropertyInfoEx(PropertyInfo property) {
            _property = property;
        }

        /// <summary>
        /// Является ли указанное свойство сериализуемым.
        /// </summary>
        public static bool IsSerializable(PropertyInfo property) {
            if (!property.CanRead || property.GetIndexParameters().Length != 0)
                return false;
            if (property.GetGetMethod() != null)
                return !property.HasAttribute<NotSerializeAttribute>();
            else
                return property.HasAttribute<SerializeAttribute>();
        }

        /// <summary>
        /// Оригинальная информация о свойстве.
        /// </summary>
        public PropertyInfo PropertyInfo {
            get { return _property; }
        }

        /// <summary>
        /// Стандартная информация о свойстве.
        /// </summary>
        public override MemberInfo MemberInfo {
            get { return _property; }
        }

        /// <summary>
        /// Тип свойства.
        /// </summary>
        public override Type MemberType {
            get { return _property.PropertyType; }
        }

        /// <summary>
        /// Свойство только для чтения, если в нем отсутствует сеттер.
        /// </summary>
        public override bool IsReadOnly {
            get { return !_property.CanWrite || _property.GetSetMethod(true) == null; }
        }

        /// <summary>
        /// Создает делегаты доступа к свойству.
        /// </summary>
        public override MemberAccessors<T, V> GetAccessors<T, V>(IAccessorFactory factory) {
            if (!typeof(T).IsValueType)
                return new MemberAccessors<T, V>(factory.CreateGetter<T, V>(_property), IsReadOnly ? null : factory.CreateSetter<T, V>(_property));
            else
                return new MemberAccessors<T, V>(factory.CreateRefGetter<T, V>(_property), IsReadOnly ? null : factory.CreateRefSetter<T, V>(_property));
        }
    }
}
