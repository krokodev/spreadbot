/*
 *  $Id: ListTypeInfo.cs 175 2010-11-20 12:33:47Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;

using Nereal.Extensions;

namespace Nereal.Serialization.TypeInfos {
    /// <summary>
    /// Информация о списках.
    /// Параметром типа является элемент списка, а не сам список.
    /// </summary>
    public class ListTypeInfo<T> : TypeInfo<List<T>> {
        private TypeInfo<T> _itemInfo;

        /// <summary>
        /// Инициализирует информацию о типе элемента списка.
        /// </summary>
        protected override void Initialize() {
            _itemInfo = SerializationConfig.InfoManager.GetInfo<T>();
        }

        /// <summary>
        /// Список может быть обновлен.
        /// </summary>
        public override bool IsUpdateable {
            get { return true; }
        }

        /// <summary>
        /// Список является коллекцией.
        /// </summary>
        public override bool IsCollection {
            get { return true; }
        }

        /// <summary>
        /// Проверяет, могут ли элементы списка быть в строчном виде.
        /// </summary>
        public override void TestMember(MemberWrapper member) {
            EnsureInitialize();
            if (member.IsInlineList && !_itemInfo.CanInlineToList)
                throw SerializationException.NotInlineList(member);
        }

        /// <summary>
        /// Список сохраняется в виде атрибута, если он строчный. Иначе в виде элемента.
        /// </summary>
        public override bool IsElement(MemberWrapper member) {
            return !member.IsInlineList;
        }

        /// <summary>
        /// Создает новый список.
        /// </summary>
        public override List<T> CreateNew() {
            return new List<T>();
        }

        /// <summary>
        /// Клонирует список с клонированием всех элементов.
        /// </summary>
        public override List<T> Clone(List<T> value) {
            if (value == null)
                return null;
            EnsureInitialize();
            var newValue = CreateNew();
            for (int i = 0; i < value.Count; i++)
                newValue.Add(value[i].DeepClone(_itemInfo));
            return newValue;
        }

        /// <summary>
        /// Сравнивает два списка.
        /// </summary>
        public override bool Equals(List<T> a, List<T> b) {
            if (a == b)
                return true;
            if (a == null || b == null || a.Count != b.Count)
                return false;
            EnsureInitialize();
            for (int i = 0; i < a.Count; i++)
                if (!a[i].DeepEquals(b[i], _itemInfo))
                    return false;
            return true;
        }

        /// <summary>
        /// Проверяет список на null.
        /// </summary>
        public override bool IsNull(List<T> value) {
            return value == null;
        }

        /// <summary>
        /// Десериализует список.
        /// </summary>
        public override List<T> Deserialize(MemberWrapper member, DeserializationContext context) {
            var value = CreateNew();
            if (member.IsInlineList) {
                ConvertFromString(context.ReadText(), value);
            } else {
                Deserialize(member, context, value);
            }
            return value;
        }

        /// <summary>
        /// Десериализует существующий список.
        /// </summary>
        public override void Deserialize(MemberWrapper member, DeserializationContext context, List<T> value) {
            value.Clear();
            var itemMember = member.CreateItem<T>();
            value.AddRange(context.ReadElements<T>(itemMember));
        }

        /// <summary>
        /// Сериализует список.
        /// </summary>
        public override void Serialize(MemberWrapper member, SerializationContext context, List<T> value) {
            if (member.IsInlineList) {
                context.WriteText(ConvertToString(value));
            } else {
                var itemMember = member.CreateItem<T>();
                context.WriteElements(itemMember, value);
            }
        }

        /// <summary>
        /// Конвертирует строку в список.
        /// </summary>
        public override List<T> ConvertFromString(string value) {
            var list = CreateNew();
            ConvertFromString(value, list);
            return list;
        }

        /// <summary>
        /// Конвертирует строку в существующий список.
        /// </summary>
        public override void ConvertFromString(string stringValue, List<T> value) {
            EnsureInitialize();
            InlineListUtility.UpdateList(stringValue, _itemInfo, value);
        }

        /// <summary>
        /// Конвертирует список в строку.
        /// </summary>
        public override string ConvertToString(List<T> value) {
            EnsureInitialize();
            return InlineListUtility.GetString(value, _itemInfo);
        }
    }

    /// <summary>
    /// Селектор класса информации о списках.
    /// </summary>
    [TypeInfoSelector]
    public class ListTypeInfoSelector : ITypeInfoSelector {
        /// <summary>
        /// Третий приоритет (конкретные объекты структур данных).
        /// </summary>
        public int Priority {
            get { return 20; }
        }

        /// <summary>
        /// Проверяет, что тип является списком.
        /// </summary>
        public bool Accept(Type objectType) {
            return objectType.IsGenericFrom(typeof(List<>));
        }

        /// <summary>
        /// Получает класс информации о списке.
        /// </summary>
        public Type GetTypeInfo(Type objectType) {
            var parameters = objectType.GetGenericArguments();
            return typeof(ListTypeInfo<>).MakeGenericType(parameters[0]);
        }
    }
}
