/*
 *  $Id: MemberWrapper.cs 187 2010-11-29 14:52:37Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;
using System.Reflection;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Обертка над полем или свойством, содержащая всю информацию, необходимую для его (де)сериализации.
    /// </summary>
    public class MemberWrapper {
        /// <summary>
        /// Имя подэлемента по умолчанию.
        /// </summary>
        public const string DefaultItemName = "item";
        /// <summary>
        /// Имя ключа по умолчанию.
        /// </summary>
        public const string DefaultKeyName = "key";

        private MemberInfo _info;
        private Type _memberType;
        private string _name, _itemName, _keyName;
        private bool _isReadOnly, _isKeyed, _isElement, _isInlineList;
        private int _order;

        /// <summary>
        /// Создает новый экземпляр обертки над полем/свойством по информации об этих поле/свойстве.
        /// </summary>
        protected MemberWrapper(MemberInfoEx member) {
            _info = member.MemberInfo;
            _memberType = member.MemberType;
            _isReadOnly = member.IsReadOnly;
            UpdateName();
            UpdateKeyName();
            _order = _info.GetAttributeValue<MemberOrderAttribute, int>(true, attr => attr.Order, int.MaxValue);
            _isElement = _info.HasAttribute<XmlStoreAsElementAttribute>(true);
            _isInlineList = _info.HasAttribute<XmlInlineListAttribute>(true);
        }
        /// <summary>
        /// Создает новый экземпляр псевдо-обертки с прямым указанием основных параметров.
        /// </summary>
        internal MemberWrapper(Type memberType, bool isElement, string name, string itemName) {
            _info = null;
            _memberType = memberType;
            _isElement = isElement;
            _name = string.IsNullOrEmpty(name) ? null : name;
            _itemName = string.IsNullOrEmpty(itemName) ? null : itemName;
            _keyName = DefaultKeyName;
            _isKeyed = false;
            _order = int.MaxValue;
        }

        /// <summary>
        /// Создает псевдо-обертку для подэлемента.
        /// </summary>
        public static MemberWrapper CreateItem<T>(string itemName) {
            return new MemberWrapper(typeof(T), true, itemName, itemName);
        }
        /// <summary>
        /// Создает псевдо-обертку для подэлемента.
        /// </summary>
        public MemberWrapper CreateItem<T>() {
            return CreateItem<T>(_itemName);
        }
        /// <summary>
        /// Создает псевдо-обертку для ключа.
        /// </summary>
        public MemberWrapper CreateKey<T>() {
            return new MemberWrapper(typeof(T), _isElement, KeyName, null);
        }

        /// <summary>
        /// Стандартная информация о поле/свойстве.
        /// Равно null, если это псевдо-обертка.
        /// </summary>
        protected MemberInfo MemberInfo {
            get { return _info; }
        }

        /// <summary>
        /// Тип, в котором объявлено поле/свойство.
        /// </summary>
        public Type DeclaringType {
            get { return _info != null ? _info.DeclaringType : null; }
        }
        /// <summary>
        /// Тип поля/свойства.
        /// </summary>
        public Type MemberType {
            get { return _memberType; }
        }

        /// <summary>
        /// Является ли поле/свойство только для чтения.
        /// </summary>
        public bool IsReadOnly {
            get { return _isReadOnly; }
        }
        /// <summary>
        /// Настоящее имя поля/свойства.
        /// </summary>
        public string MemberName {
            get { return _info != null ? _info.Name : Name; }
        }
        /// <summary>
        /// Xml-имя поля/свойства.
        /// </summary>
        public string Name {
            get { return _name ?? (_info != null ? _info.Name : string.Empty); }
        }
        /// <summary>
        /// Xml-имя подэлементов.
        /// </summary>
        public string ItemName {
            get { return _itemName ?? DefaultItemName; }
        }
        /// <summary>
        /// Xml-имя ключа.
        /// </summary>
        public string KeyName {
            get { return _keyName; }
        }

        /// <summary>
        /// Исходно установленное xml-имя поля/свойства (может быть равно null).
        /// </summary>
        public string OriginalName {
            get { return _name; }
        }
        /// <summary>
        /// Исходно установленное xml-имя подэлементов поля/свойства (может быть равно null).
        /// </summary>
        public string OriginalItemName {
            get { return _itemName; }
        }

        /// <summary>
        /// Является ли поле/свойство xml-элементом (без учета типа значения).
        /// </summary>
        public bool DefaultIsElement {
            get { return _isElement; }
        }

        /// <summary>
        /// Является ли поле/свойство xml-элементом (с учетом типа значения).
        /// </summary>
        public virtual bool IsElement {
            get { return _isElement; }
        }

        /// <summary>
        /// Являются ли подэлементы идентифицируемыми по ключу.
        /// </summary>
        public bool IsKeyed {
            get { return _isKeyed; }
        }

        /// <summary>
        /// Порядковый номер поля/свойства.
        /// </summary>
        public int Order {
            get { return _order; }
        }

        /// <summary>
        /// Является ли поле/свойство строчным списком.
        /// </summary>
        public bool IsInlineList {
            get { return _isInlineList; }
        }

        /// <summary>
        /// Обновляет свойства имён.
        /// </summary>
        private void UpdateName() {
            var attr = _info.GetAttribute<XmlNameAttribute>(true);
            if (attr != null) {
                _name = attr.Name;
                _itemName = attr.ItemName;
            } else {
                _name = _info.Name;
                _itemName = DefaultItemName;
            }
        }

        /// <summary>
        /// Обновляет свойство имени ключа.
        /// </summary>
        private void UpdateKeyName() {
            var attr = _info.GetAttribute<KeyNameAttribute>(true);
            _keyName = attr != null && !string.IsNullOrEmpty(attr.KeyName) ? attr.KeyName : DefaultKeyName;
            _isKeyed = _info.HasAttribute<KeyedValueAttribute>(true);
        }

        /// <summary>
        /// Получает строковое представление полного имени поля/свойства.
        /// </summary>
        public override string ToString() {
            if (DeclaringType != null)
                return string.Format("'{0}.{1}'", DeclaringType, MemberName);
            else
                return MemberName;
        }
    }
}
