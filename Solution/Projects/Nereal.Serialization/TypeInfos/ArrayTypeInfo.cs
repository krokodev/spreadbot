/*
 *  $Id: ArrayTypeInfo.cs 175 2010-11-20 12:33:47Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;

namespace Nereal.Serialization.TypeInfos {
    /// <summary>
    /// Информация о массивах.
    /// Параметром типа является элемент массива, а не сам массив.
    /// </summary>
    public class ArrayTypeInfo<T> : TypeInfo<T[]> {
        private TypeInfo<T> _itemInfo;

        /// <summary>
        /// Инициализирует информацию о типе элемента массива.
        /// </summary>
        protected override void Initialize() {
            _itemInfo = SerializationConfig.InfoManager.GetInfo<T>();
        }

        /// <summary>
        /// Массив является коллекцией.
        /// </summary>
        public override bool IsCollection {
            get { return true; }
        }

        /// <summary>
        /// Проверяет, могут ли элементы массива быть в строчном виде.
        /// </summary>
        public override void TestMember(MemberWrapper member) {
            EnsureInitialize();
            if (member.IsInlineList && !_itemInfo.CanInlineToList)
                throw SerializationException.NotInlineList(member);
        }

        /// <summary>
        /// Массив сохраняется в виде атрибута, если он строчный. Иначе в виде элемента.
        /// </summary>
        public override bool IsElement(MemberWrapper member) {
            return !member.IsInlineList;
        }

        /// <summary>
        /// Создает новый пустой массив.
        /// </summary>
        public override T[] CreateNew() {
            return new T[0];
        }

        /// <summary>
        /// Клонирует массив с клонированием всех элементов.
        /// </summary>
        public override T[] Clone(T[] value) {
            if (value == null)
                return null;
            EnsureInitialize();
            var newValue = new T[value.Length];
            for (int i = 0; i < value.Length; i++)
                newValue[i] = value[i].DeepClone(_itemInfo);
            return newValue;
        }

        /// <summary>
        /// Сравнивает два массива.
        /// </summary>
        public override bool Equals(T[] a, T[] b) {
            if (a == b)
                return true;
            if (a == null || b == null || a.Length != b.Length)
                return false;
            EnsureInitialize();
            for (int i = 0; i < a.Length; i++)
                if (!a[i].DeepEquals(b[i], _itemInfo))
                    return false;
            return true;
        }

        /// <summary>
        /// Проверяет массив на null.
        /// </summary>
        public override bool IsNull(T[] value) {
            return value == null;
        }

        /// <summary>
        /// Десериализует массив.
        /// </summary>
        public override T[] Deserialize(MemberWrapper member, DeserializationContext context) {
            if (member.IsElement) {
                var itemMember = member.CreateItem<T>();
                var list = new List<T>(context.ReadElements<T>(itemMember));
                return list.ToArray();
            } else {
                return ConvertFromString(context.ReadText());
            }
        }

        /// <summary>
        /// Сериализует массив.
        /// </summary>
        public override void Serialize(MemberWrapper member, SerializationContext context, T[] value) {
            if (member.IsElement) {
                var itemMember = member.CreateItem<T>();
                context.WriteElements(itemMember, value);
            } else {
                context.WriteText(ConvertToString(value));
            }
        }

        /// <summary>
        /// Конвертирует строку в массив.
        /// </summary>
        public override T[] ConvertFromString(string value) {
            EnsureInitialize();
            return InlineListUtility.GetArray(value, _itemInfo);
        }

        /// <summary>
        /// Конвертирует массив в строку.
        /// </summary>
        public override string ConvertToString(T[] value) {
            EnsureInitialize();
            return InlineListUtility.GetString(value, _itemInfo);
        }
    }

    /// <summary>
    /// Селектор класса информации о массивах.
    /// </summary>
    [TypeInfoSelector]
    public class ArrayTypeInfoSelector : ITypeInfoSelector {
        /// <summary>
        /// Первый приоритет (массивы).
        /// </summary>
        public int Priority {
            get { return 0; }
        }

        /// <summary>
        /// Проверяет, что тип является массивом.
        /// </summary>
        public bool Accept(Type objectType) {
            return objectType.IsArray;
        }

        /// <summary>
        /// Получает класс информации о массиве.
        /// </summary>
        public Type GetTypeInfo(Type objectType) {
            return typeof(ArrayTypeInfo<>).MakeGenericType(objectType.GetElementType());
        }
    }
}