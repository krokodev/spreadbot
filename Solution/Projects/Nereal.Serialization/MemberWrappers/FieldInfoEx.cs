/*
 *  $Id: FieldInfoEx.cs 180 2010-11-26 09:25:29Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Reflection;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Расширенная информация о поле.
    /// </summary>
    internal sealed class FieldInfoEx : MemberInfoEx {
        private FieldInfo _field;

        public FieldInfoEx(FieldInfo field) {
            _field = field;
        }

        /// <summary>
        /// Является ли указанное поле сериализуемым.
        /// </summary>
        public static bool IsSerializable(FieldInfo field) {
            if (field.IsPublic)
                return !field.HasAttribute<NotSerializeAttribute>();
            else
                return field.HasAttribute<SerializeAttribute>();
        }

        /// <summary>
        /// Стандартная информация о поле.
        /// </summary>
        public override MemberInfo MemberInfo {
            get { return _field; }
        }

        /// <summary>
        /// Тип поля.
        /// </summary>
        public override Type MemberType {
            get { return _field.FieldType; }
        }

        /// <summary>
        /// Поле только для чтения, если оно только для инициализации.
        /// </summary>
        public override bool IsReadOnly {
            get { return _field.IsInitOnly; }
        }

        /// <summary>
        /// Создает делегаты доступа к полю.
        /// </summary>
        public override MemberAccessors<T, V> GetAccessors<T, V>(IAccessorFactory factory) {
            if (!typeof(T).IsValueType)
                return new MemberAccessors<T, V>(factory.CreateGetter<T, V>(_field), IsReadOnly ? null : factory.CreateSetter<T, V>(_field));
            else
                return new MemberAccessors<T, V>(factory.CreateRefGetter<T, V>(_field), IsReadOnly ? null : factory.CreateRefSetter<T, V>(_field));
        }
    }
}
