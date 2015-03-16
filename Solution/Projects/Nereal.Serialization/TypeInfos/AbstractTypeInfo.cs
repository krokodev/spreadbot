/*
 *  $Id: AbstractTypeInfo.cs 175 2010-11-20 12:33:47Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Абстрактный базовый класс информации о типе.
    /// Выделен отдельно только из-за необходимости хранить общий список TypeInfo.
    /// </summary>
    public abstract class AbstractTypeInfo {
        private bool _initialized;
        private Type _type;
        private string _rootName;
        private MemberWrapper _rootWrapper;

        /// <summary>
        /// Инициализирует новый экземпляр информации о типе.
        /// </summary>
        public AbstractTypeInfo(Type type) {
            _initialized = false;
            _type = type;
            UpdateRootName();
        }

        /// <summary>
        /// Тип, о котором информация.
        /// </summary>
        public Type ThisType {
            get { return _type; }
        }

        /// <summary>
        /// Имя корневого элемента в XML, если сохраняется/загружается данный тип.
        /// </summary>
        public string RootName {
            get { return _rootName; }
        }

        /// <summary>
        /// Псевдо-обертка для корневого элемента в XML, если сохраняется/загружается данный тип.
        /// </summary>
        public MemberWrapper RootWrapper {
            get { return _rootWrapper; }
        }

        /// <summary>
        /// Инициализирует информацию о типе, если она еще не была инициализирована.
        /// </summary>
        public void EnsureInitialize() {
            if (_initialized)
                return;
            _initialized = true;
            Initialize();
        }

        /// <summary>
        /// Инициализирует информацию о типе.
        /// </summary>
        protected virtual void Initialize() {
        }

        /// <summary>
        /// True, если данный тип не может иметь потомков.
        /// </summary>
        public virtual bool IsSealed {
            get { return ThisType.IsSealed; }
        }

        /// <summary>
        /// True, если данный тип может быть десериализован обновлением.
        /// </summary>
        public virtual bool IsUpdateable {
            get { return false; }
        }

        /// <summary>
        /// Может ли данный тип быть сохранен в строчном списке.
        /// </summary>
        public virtual bool CanInlineToList {
            get { return false; }
        }

        /// <summary>
        /// Является ли данный тип коллекцией (и может ли опускать групповой xml-элемент.
        /// </summary>
        public virtual bool IsCollection {
            get { return false; }
        }

        /// <summary>
        /// Проверяет корректность указанной обертки над полем свойством для данного типа.
        /// В случае ошибок, выдает исключение.
        /// </summary>
        public virtual void TestMember(MemberWrapper member) {
        }

        /// <summary>
        /// Проверяет, нужно ли сохранять данный тип как элемент, с учетом поля/свойства.
        /// </summary>
        /// <remarks>
        /// В этом методе нельзя вызывать member.IsElement, так как это грозит бесконечной рекурсией.
        /// </remarks>
        public virtual bool IsElement(MemberWrapper member) {
            return false;
        }

        /// <summary>
        /// Обновляет свойства имени корневого элемента.
        /// </summary>
        private void UpdateRootName() {
            var attr = _type.GetAttribute<XmlRootNameAttribute>(true);
            _rootName = attr != null ? attr.RootName : _type.Name;
            _rootWrapper = new MemberWrapper(_type, true, _rootName, null);
        }
    }
}

