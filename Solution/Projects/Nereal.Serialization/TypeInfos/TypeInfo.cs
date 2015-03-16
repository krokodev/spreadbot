/*
 *  $Id: TypeInfo.cs 190 2010-12-02 13:20:50Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Абстрактный типизированный класс информации о типе.
    /// Включает все основные методы операций над объектами данного типа.
    /// </summary>
    public abstract class TypeInfo<T> : AbstractTypeInfo {
        private bool _hasDeserializeEvents, _hasSerializeEvents;
        private List<DefaultReferenceResolver<T>> _resolvers;
        private Dictionary<Type, TypeInfo<T>> _adapters;

        /// <summary>
        /// Инициализирует новый экземлпяр информации о типе.
        /// </summary>
        public TypeInfo() : base(typeof(T)) {
            _hasDeserializeEvents = ThisType.Implements(typeof(IDeserializeEvents));
            _hasSerializeEvents = ThisType.Implements(typeof(ISerializeEvents));
            _resolvers = new List<DefaultReferenceResolver<T>>(DefaultReferenceResolver<T>.Create());
            _adapters = null;
        }

        /// <summary>
        /// Обрабатывает событие начала десериализации объекта.
        /// </summary>
        protected void OnBeforeDeserialize(DeserializationContext context, T value) {
            for (int i = 0; i < _resolvers.Count; i++)
                _resolvers[i].OnBeforeDeserialize(context, value);
            if (_hasDeserializeEvents)
                (value as IDeserializeEvents).OnBeforeDeserialize(context);
        }

        /// <summary>
        /// Обрабатывает событие окончания десериализации объекта.
        /// </summary>
        protected void OnAfterDeserialize(DeserializationContext context, T value) {
            if (_hasDeserializeEvents)
                (value as IDeserializeEvents).OnAfterDeserialize(context);
            for (int i = _resolvers.Count - 1; i >= 0; i--)
                _resolvers[i].OnAfterDeserialize(context, value);
        }

        /// <summary>
        /// Обрабатывает событие начала сериализации объекта.
        /// </summary>
        protected void OnBeforeSerialize(SerializationContext context, T value) {
            for (int i = 0; i < _resolvers.Count; i++)
                _resolvers[i].OnBeforeSerialize(context, value);
            if (_hasSerializeEvents)
                (value as ISerializeEvents).OnBeforeSerialize(context);
        }

        /// <summary>
        /// Обрабатывает событие окончания сериализации объекта.
        /// </summary>
        protected void OnAfterSerialize(SerializationContext context, T value) {
            if (_hasSerializeEvents)
                (value as ISerializeEvents).OnAfterSerialize(context);
            for (int i = _resolvers.Count - 1; i >= 0; i--)
                _resolvers[i].OnAfterSerialize(context, value);
        }

        #region Abstract methods and base type operations
        /// <summary>
        /// Создает новый объект данного типа.
        /// </summary>
        public virtual T CreateNew() {
            return default(T);
        }

        /// <summary>
        /// Клонирует указанный объект данного типа.
        /// </summary>
        public abstract T Clone(T value);

        /// <summary>
        /// Сравнивает два объекта, возвращая true при равенстве.
        /// </summary>
        public abstract bool Equals(T a, T b);

        /// <summary>
        /// Проверяет указанный объект на null.
        /// </summary>
        public virtual bool IsNull(T value) {
            return false;
        }

        /// <summary>
        /// Проверяет необходимость сериализация указанного объекта.
        /// </summary>
        public virtual bool NeedSerialize(T value, bool hasDefaultValue, T defaultValue) {
            return !IsNull(value);
        }

        /// <summary>
        /// Десериализует объект по указанной обертке над полем/свойством.
        /// </summary>
        public virtual T Deserialize(MemberWrapper member, DeserializationContext context) {
            return ConvertFromString(context.ReadText());
        }

        /// <summary>
        /// Десериализует существующий объект по указанной обертке над полем/свойством, обновляя его.
        /// По умолчанию ничего не делает, должно быть перекрыто в обновляемых типах.
        /// </summary>
        public virtual void Deserialize(MemberWrapper member, DeserializationContext context, T value) {
            ConvertFromString(context.ReadText(), value);
        }

        /// <summary>
        /// Сериализует объект по указанной обертке над полем/свойством.
        /// </summary>
        public virtual void Serialize(MemberWrapper member, SerializationContext context, T value) {
            context.WriteText(ConvertToString(value));
        }

        /// <summary>
        /// Конвертирует строку в объект данного типа.
        /// </summary>
        public virtual T ConvertFromString(string value) {
            return (T) Convert.ChangeType(value, ThisType);
        }

        /// <summary>
        /// Конвертирует строку в существующий объект, обновляя его.
        /// По умолчанию ничего не делает, должно быть перекрыто в обновляемых типах.
        /// </summary>
        public virtual void ConvertFromString(string stringValue, T value) {
        }

        /// <summary>
        /// Конвертирует объект данного типа в строку.
        /// </summary>
        public virtual string ConvertToString(T value) {
            return value.ToString();
        }
        #endregion

        #region TypeInfo adapters
        /// <summary>
        /// Создает словарь адаптеров, если он еще не создан.
        /// </summary>
        private void EnsureAdapters() {
            if (_adapters == null)
                _adapters = new Dictionary<Type, TypeInfo<T>>();
        }

        /// <summary>
        /// Получает адаптированную к указанному типу информацию.
        /// </summary>
        internal TypeInfo<T> GetAdapter(Type objectType) {
            if (objectType == ThisType)
                return this;
            EnsureAdapters();
            TypeInfo<T> adapterInfo;
            if (!_adapters.TryGetValue(objectType, out adapterInfo)) {
                adapterInfo = CreateAdapter(objectType);
                _adapters.Add(objectType, adapterInfo);
                adapterInfo.Initialize();
            }
            return adapterInfo;
        }

        /// <summary>
        /// Создает новый адаптер к указанному типу.
        /// </summary>
        private TypeInfo<T> CreateAdapter(Type objectType) {
            if (!ThisType.IsAssignableFrom(objectType))
                throw new SerializationException(string.Format("Adapted TypeInfo from '{0}' to '{1}' cannot be created.", objectType, ThisType));
            var infoType = typeof(AdaptedTypeInfo<, >).MakeGenericType(ThisType, objectType);
            return (TypeInfo<T>) Activator.CreateInstance(infoType);
        }
        #endregion
    }
}
