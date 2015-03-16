/*
 *  $Id: AdaptedTypeInfo.cs 190 2010-12-02 13:20:50Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Адаптированная информация о типе: выглядит как информация о базовом типе (предке реального типа), перенаправляет все операции в информацию о реальном типе.
    /// </summary>
    public class AdaptedTypeInfo<T, TReal> : TypeInfo<T> where TReal : T {
        private TypeInfo<TReal> _realInfo;

        /// <summary>
        /// Создает новый экземпляр адаптированной информации.
        /// </summary>
        public AdaptedTypeInfo() {
            _realInfo = SerializationConfig.InfoManager.GetInfo<TReal>();
        }

        /// <summary>
        /// Перенаправляет запрос в реальный тип.
        /// </summary>
        public override bool IsUpdateable {
            get { return _realInfo.IsUpdateable; }
        }

        /// <summary>
        /// Перенаправляет вызов в реальный тип.
        /// </summary>
        public override bool IsElement(MemberWrapper member) {
            return _realInfo.IsElement(member);
        }

        /// <summary>
        /// Перенаправляет вызов в реальный тип.
        /// </summary>
        public override void TestMember(MemberWrapper member) {
            _realInfo.TestMember(member);
        }

        /// <summary>
        /// Перенаправляет вызов в реальный тип.
        /// </summary>
        public override T CreateNew() {
            return _realInfo.CreateNew();
        }

        /// <summary>
        /// Перенаправляет вызов в реальный тип.
        /// </summary>
        public override T Clone(T obj) {
            return _realInfo.Clone((TReal) obj);
        }

        /// <summary>
        /// Перенаправляет вызов в реальный тип.
        /// </summary>
        public override bool Equals(T a, T b) {
            return _realInfo.Equals((TReal) a, (TReal) b);
        }

        /// <summary>
        /// Перенаправляет вызов в реальный тип.
        /// </summary>
        public override bool IsNull(T value) {
            return _realInfo.IsNull((TReal) value);
        }

        /// <summary>
        /// Перенаправляет вызов в реальный тип.
        /// </summary>
        public override bool NeedSerialize(T value, bool hasDefaultValue, T defaultValue) {
            return _realInfo.NeedSerialize((TReal) value, hasDefaultValue, (TReal) defaultValue);
        }

        /// <summary>
        /// Перенаправляет вызов в реальный тип.
        /// </summary>
        public override T Deserialize(MemberWrapper member, DeserializationContext context) {
            return _realInfo.Deserialize(member, context);
        }

        /// <summary>
        /// Перенаправляет вызов в реальный тип.
        /// </summary>
        public override void Serialize(MemberWrapper member, SerializationContext context, T obj) {
            _realInfo.Serialize(member, context, (TReal) obj);
        }

        /// <summary>
        /// Перенаправляет вызов в реальный тип.
        /// </summary>
        public override T ConvertFromString(string value) {
            return _realInfo.ConvertFromString(value);
        }

        /// <summary>
        /// Перенаправляет вызов в реальный тип.
        /// </summary>
        public override string ConvertToString(T value) {
            return _realInfo.ConvertToString((TReal) value);
        }
    }
}
