/*
 *  $Id: MemberValue.cs 155 2010-11-03 21:31:02Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Абстрактная обертка над значением поля или свойства, без учета типа значения.
    /// </summary>
    public abstract class MemberValue<T> {
        /// <summary>
        /// Обертка над полем/свойством.
        /// </summary>
        public abstract TypeMemberWrapper<T> Member { get; }
        /// <summary>
        /// Значение в виде object.
        /// </summary>
        public abstract object Value { get; }

        /// <summary>
        /// Устаналивает значение поля/свойства в указанный объект.
        /// </summary>
        public abstract void SetValue(T obj);
        /// <summary>
        /// Устаналивает значение поля/свойства в указанный объект, с передачей объекта по ссылке (для структур).
        /// </summary>
        public abstract void SetValue(ref T obj);
    }

    /// <summary>
    /// Обертка над значением поля или свойства, с учетом типа значения.
    /// </summary>
    public class MemberValue<T, V> : MemberValue<T> {
        private ValueMemberWrapper<T, V> _member;
        // TODO : remove no exists values from list
        private bool _exists;
        private V _value;

        /// <summary>
        /// Создает новый экземпляр обертки над значением.
        /// </summary>
        public MemberValue(ValueMemberWrapper<T, V> member, bool exists, V value) {
            _member = member;
            _exists = exists;
            _value = value;
        }

        /// <summary>
        /// Обертка над полем/свойством.
        /// </summary>
        public override TypeMemberWrapper<T> Member {
            get { return _member; }
        }
        /// <summary>
        /// Значение в виде object.
        /// </summary>
        public override object Value {
            get { return _value; }
        }

        /// <summary>
        /// Устанавливает значение поля/свойства в указанный объект.
        /// </summary>
        public override void SetValue(T obj) {
            if (_exists)
                _member.SetValue(obj, _value);
        }
        /// <summary>
        /// Устанавливает значение поля/свойства в указанный объект, с передачей объекта по ссылке (для структур).
        /// </summary>
        public override void SetValue(ref T obj) {
            if (_exists)
                _member.SetValue(ref obj, _value);
        }
    }
}
