/*
 *  $Id: DictionaryTypeInfo.cs 175 2010-11-20 12:33:47Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;

using Nereal.Extensions;

namespace Nereal.Serialization.TypeInfos {
    /// <summary>
    /// Информация о словаре.
    /// Параметрами типа являются ключ и значение, а не сам словарь.
    /// </summary>
    /// <typeparam name="TKey">Тип ключа словаря.</typeparam>
    /// <typeparam name="TValue">Тип значения словаря.</typeparam>
    public class DictionaryTypeInfo<TKey, TValue> : TypeInfo<Dictionary<TKey, TValue>> {
        private static readonly Type KeyedInterfaceType = typeof(IKeyed<TKey>);
        private static readonly Type ValueType = typeof(TValue);

        private TypeInfo<TKey> _keyInfo;
        private TypeInfo<TValue> _valueInfo;

        /// <summary>
        /// Инициализирует информацию о типе ключа словаря.
        /// </summary>
        protected override void Initialize() {
            _keyInfo = SerializationConfig.InfoManager.GetInfo<TKey>();
            _valueInfo = SerializationConfig.InfoManager.GetInfo<TValue>();
        }

        /// <summary>
        /// Словарь может быть обновлен.
        /// </summary>
        public override bool IsUpdateable {
            get { return true; }
        }

        /// <summary>
        /// Словарь является коллекцией.
        /// </summary>
        public override bool IsCollection {
            get { return true; }
        }

        /// <summary>
        /// Словарь может быть сохранен только в виде элемента.
        /// </summary>
        public override bool IsElement(MemberWrapper member) {
            return true;
        }

        /// <summary>
        /// Проверяет корректность поля/свойства словаря.
        /// Если оно указано как KeyedValue, то тип значений словаря должен реализовывать интерфейс <see cref="T:Nereal.Extensions.IKeyed`1{`0}" />.
        /// </summary>
        public override void TestMember(MemberWrapper member) {
            if (member.IsKeyed) {
                if (!ValueType.Implements(KeyedInterfaceType))
                    throw SerializationException.NotImplements(ValueType, KeyedInterfaceType);
            }
        }

        /// <summary>
        /// Создает новый словарь.
        /// </summary>
        public override Dictionary<TKey, TValue> CreateNew() {
            return new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Клонирует словарь с клонированием всех ключей и значений.
        /// </summary>
        public override Dictionary<TKey, TValue> Clone(Dictionary<TKey, TValue> value) {
            if (value == null)
                return null;
            EnsureInitialize();
            var newValue = CreateNew();
            foreach (var item in value)
                newValue.Add(item.Key.DeepClone(_keyInfo), item.Value.DeepClone(_valueInfo));
            return newValue;
        }

        /// <summary>
        /// Сравнивает два словаря.
        /// </summary>
        public override bool Equals(Dictionary<TKey, TValue> a, Dictionary<TKey, TValue> b) {
            if (a == b)
                return true;
            if (a == null || b == null || a.Count != b.Count)
                return false;
            EnsureInitialize();
            foreach (var item in a)
                if (!b.ContainsKey(item.Key) || !item.Value.DeepEquals(b[item.Key], _valueInfo))
                    return false;
            return true;
        }

        /// <summary>
        /// Проверяет словарь на null.
        /// </summary>
        public override bool IsNull(Dictionary<TKey, TValue> value) {
            return value == null;
        }

        /// <summary>
        /// Десериализует словарь.
        /// </summary>
        public override Dictionary<TKey, TValue> Deserialize(MemberWrapper member, DeserializationContext context) {
            var dict = CreateNew();
            Deserialize(member, context, dict);
            return dict;
        }

        /// <summary>
        /// Десериализует существующий словарь.
        /// </summary>
        public override void Deserialize(MemberWrapper member, DeserializationContext context, Dictionary<TKey, TValue> value) {
            EnsureInitialize();
            var itemMember = member.CreateItem<TValue>();
            foreach (var ctx in context.ReadElements(true)) {
                var key = default(TKey);
                var item = default(TValue);
                if (member.IsKeyed) {
                    item = ctx.ReadElement<TValue>(itemMember);
                    key = ((IKeyed<TKey>) item).Key;
                } else {
                    key = _keyInfo.ConvertFromString(ctx.ReadAttribute(member.KeyName));
                    item = ctx.ReadElement<TValue>(itemMember);
                }
                value.Add(key, item);
            }
        }

        /// <summary>
        /// Сериализует словарь.
        /// </summary>
        public override void Serialize(MemberWrapper member, SerializationContext context, Dictionary<TKey, TValue> value) {
            EnsureInitialize();
            var itemMember = member.CreateItem<TValue>();
            foreach (var item in value) {
                var objectType = item.Value.GetType();
                context.StartElement(context.GetElementName(itemMember, objectType));
                if (!member.IsKeyed)
                    context.WriteAttribute(member.KeyName, _keyInfo.ConvertToString(item.Key));
                context.WriteType(itemMember, objectType);
                objectType.GetInfo<TValue>().Serialize(itemMember, context, item.Value);
                context.EndElement();
            }
        }

        /// <summary>
        /// Ничего не делает, так как словарь не сериализуется в строку.
        /// </summary>
        public override void ConvertFromString(string stringValue, Dictionary<TKey, TValue> value) {
        }
    }

    /// <summary>
    /// Селектор класса информации о словарях.
    /// </summary>
    [TypeInfoSelector]
    public class DictionaryTypeInfoSelector : ITypeInfoSelector {
        /// <summary>
        /// Третий приоритет (конкретные объекты структур данных).
        /// </summary>
        public int Priority {
            get { return 20; }
        }

        /// <summary>
        /// Проверяет, что тип является словарем.
        /// </summary>
        public bool Accept(Type objectType) {
            return objectType.IsGenericFrom(typeof(Dictionary<, >));
        }

        /// <summary>
        /// Получает класс информации о словаре.
        /// </summary>
        public Type GetTypeInfo(Type objectType) {
            var parameters = objectType.GetGenericArguments();
            return typeof(DictionaryTypeInfo<, >).MakeGenericType(parameters[0], parameters[1]);
        }
    }
}
