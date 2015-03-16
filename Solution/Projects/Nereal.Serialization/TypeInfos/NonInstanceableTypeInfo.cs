/*
 *  $Id: NonInstanceableTypeInfo.cs 160 2010-11-04 13:58:07Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization.TypeInfos {
    /// <summary>
    /// Информация о типах, не имеющих экземпляров.
    /// Является заглушкой и используется для интерфейсов и абстрактных типов.
    /// </summary>
    public class NonInstanceableTypeInfo<T> : TypeInfo<T> where T : class {
        /// <summary>
        /// Интерфейс или абстрактный тип может быть сохранен только в виде элемента, так как всегда необходимо сохранение типа.
        /// </summary>
        public override bool IsElement(MemberWrapper member) {
            return true;
        }

        /// <summary>
        /// Возвращает сам же объект, ничего не копируя.
        /// </summary>
        public override T Clone(T value) {
            return value;
        }

        /// <summary>
        /// Возвращает false, ничего не сравнивая.
        /// </summary>
        public override bool Equals(T a, T b) {
            return false;
        }

        /// <summary>
        /// Проверяет объект на null.
        /// </summary>
        public override bool IsNull(T value) {
            return value == null;
        }

        /// <summary>
        /// Возвращает null, ничего не десериализуя.
        /// </summary>
        public override T Deserialize(MemberWrapper member, DeserializationContext context) {
            return null;
        }

        /// <summary>
        /// Ничего не сериализует.
        /// </summary>
        public override void Serialize(MemberWrapper member, SerializationContext context, T value) {
        }

        /// <summary>
        /// Возвращает null, ничего не конвертируя.
        /// </summary>
        public override T ConvertFromString(string value) {
            return null;
        }

        /// <summary>
        /// Возвращает пустую строку, ничего не конвертируя.
        /// </summary>
        public override string ConvertToString(T value) {
            return string.Empty;
        }
    }

    /// <summary>
    /// Селектор класса информации об интерфейсах или абстрактных типов.
    /// </summary>
    [TypeInfoSelector]
    public class NonInstanceableTypeInfoSelector : ITypeInfoSelector {
        /// <summary>
        /// Начальный приоритет.
        /// </summary>
        public int Priority {
            get { return 0; }
        }

        /// <summary>
        /// Проверяет, что тип является интерфейсом или абстрактным типом.
        /// </summary>
        public bool Accept(Type objectType) {
            return objectType.IsInterface || objectType.IsAbstract;
        }

        /// <summary>
        /// Получает класс информации об типе.
        /// </summary>
        public Type GetTypeInfo(Type objectType) {
            return typeof(NonInstanceableTypeInfo<>).MakeGenericType(objectType);
        }
    }
}
