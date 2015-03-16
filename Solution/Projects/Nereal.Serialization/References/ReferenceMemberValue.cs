/*
 *  $Id: ReferenceMemberValue.cs 196 2010-12-05 14:14:13Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Reflection;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Обертка над значением поля или свойства, являющимся ссылкой.
    /// </summary>
    internal sealed class ReferenceMemberValue<TOwner, TMember, TKey, TValue> : MemberValue<TOwner> {
        private ValueMemberWrapper<TOwner, TMember> _member;
        private ReferenceCollector<TKey, TValue> _collector;
        private TKey _key;

        /// <summary>
        /// Создает новый экземпляр обертки над значением ссылочного поля/свойства.
        /// </summary>
        public ReferenceMemberValue(ValueMemberWrapper<TOwner, TMember> member, ReferenceCollector<TKey, TValue> collector, TKey key) {
            _member = member;
            _collector = collector;
            _key = key;
        }

        /// <summary>
        /// Обертка над полем/свойством.
        /// </summary>
        public override TypeMemberWrapper<TOwner> Member {
            get { return _member; }
        }
        /// <summary>
        /// Ввиду отсутствия значения ссылки до его определения, возвращается значение по умолчанию.
        /// </summary>
        public override object Value {
            get { return _member.DefaultValue; }
        }

        /// <summary>
        /// Вместо установки значения поля/свойства, добавляет ссылку в коллектор.
        /// </summary>
        public override void SetValue(TOwner obj) {
            if (!AddReference(obj))
                _member.SetValue(obj, _member.DefaultValue);
        }

        /// <summary>
        /// Вместо установки значения поля/свойства, добавляет ссылку в коллектор, с передачей объекта по ссылке (для структур).
        /// </summary>
        public override void SetValue(ref TOwner obj) {
            if (!AddReference(obj))
                _member.SetValue(ref obj, _member.DefaultValue);
        }

        /// <summary>
        /// Добавляет ссылку в коллектор.
        /// </summary>
        private bool AddReference(TOwner obj) {
            if (_collector == null)
                return false;
            var reference = new ReferenceWrapper<TKey, TValue>(v => _member.SetValue(obj, (TMember) (object) v), _key);
            _collector.Add(reference);
            return true;
        }
    }
}
