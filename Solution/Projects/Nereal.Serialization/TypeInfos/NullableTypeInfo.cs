/*
 *  $Id: NullableTypeInfo.cs 190 2010-12-02 13:20:50Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

using Nereal.Extensions;

namespace Nereal.Serialization.TypeInfos {
    /// <summary>
    /// Информация о nullable-типах.
    /// Параметром типа является тип значения.
    /// </summary>
    public class NullableTypeInfo<T> : TypeInfo<T?> where T : struct {
        private TypeInfo<T> _valueInfo;

        /// <summary>
        /// Инициализирует информацию о типе значения.
        /// </summary>
        protected override void Initialize() {
            _valueInfo = SerializationConfig.InfoManager.GetInfo<T>();
        }

        /// <summary>
        /// Nullable-тип сохраняется так же, как и тип значения.
        /// </summary>
        public override bool IsElement(MemberWrapper member) {
            EnsureInitialize();
            return _valueInfo.IsElement(member);
        }

        /// <summary>
        /// Клонирует nullable-тип.
        /// </summary>
        public override T? Clone(T? value) {
            EnsureInitialize();
            return value.HasValue ? _valueInfo.Clone(value.Value) : (T?) null;
        }

        /// <summary>
        /// Сравнивает два nullable-типа.
        /// </summary>
        public override bool Equals(T? a, T? b) {
            if (a.HasValue && b.HasValue) {
                EnsureInitialize();
                return _valueInfo.Equals(a.Value, b.Value);
            }
            return !a.HasValue && !b.HasValue;
        }

        /// <summary>
        /// Проверяет на null.
        /// </summary>
        public override bool IsNull(T? value) {
            return !value.HasValue;
        }

        /// <summary>
        /// Десериализует nullable-тип.
        /// </summary>
        public override T? Deserialize(MemberWrapper member, DeserializationContext context) {
            EnsureInitialize();
            return _valueInfo.Deserialize(member, context);
        }

        /// <summary>
        /// Сериализует nullable-тип.
        /// </summary>
        public override void Serialize(MemberWrapper member, SerializationContext context, T? value) {
            if (value.HasValue) {
                EnsureInitialize();
                _valueInfo.Serialize(member, context, value.Value);
            }
        }

        /// <summary>
        /// Конвертирует строку в nullable-тип.
        /// </summary>
        public override T? ConvertFromString(string value) {
            EnsureInitialize();
            return value != null ? _valueInfo.ConvertFromString(value) : (T?) null;
        }

        /// <summary>
        /// Конвертирует nullable-тип в строку.
        /// </summary>
        public override string ConvertToString(T? value) {
            EnsureInitialize();
            return value.HasValue ? _valueInfo.ConvertToString(value.Value) : null;
        }
    }

    /// <summary>
    /// Селектор класса информации о nullable-типах.
    /// </summary>
    [TypeInfoSelector]
    public class NullableTypeInfoSelector : ITypeInfoSelector {
        /// <summary>
        /// Приоритет перед значимыми типами.
        /// </summary>
        public int Priority {
            get { return 5; }
        }

        /// <summary>
        /// Проверяет, что тип является nullable-типом.
        /// </summary>
        public bool Accept(Type objectType) {
            return objectType.IsGenericFrom(typeof(Nullable<>));
        }

        /// <summary>
        /// Получает класс информации о nullable-типе.
        /// </summary>
        public Type GetTypeInfo(Type objectType) {
            var parameters = objectType.GetGenericArguments();
            return typeof(NullableTypeInfo<>).MakeGenericType(parameters[0]);
        }
    }
}
