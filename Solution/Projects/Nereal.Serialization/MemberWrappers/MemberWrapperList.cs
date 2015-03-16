/*
 *  $Id: MemberWrapperList.cs 179 2010-11-24 08:24:05Z thenn $
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
    /// Список оберток над полями и свойствами определенного типа.
    /// </summary>
    internal sealed class MemberWrapperList<T> /*: IEnumerable<TypeMemberWrapper<T>>*/ {
        private List<TypeMemberWrapper<T>> _members;
        private TypeMemberWrapper<T>[] _parameters;
        private int _attributeCount;
        private bool _hasEmptyNames;

        public MemberWrapperList() {
            _members = new List<TypeMemberWrapper<T>>();
            InitializeMembers();
            InitializeParameters();
        }

        /// <summary>
        /// Инициализирует списки оберток.
        /// </summary>
        private void InitializeMembers() {
            var elements = new List<TypeMemberWrapper<T>>();
            _hasEmptyNames = false;
            foreach (var m in MemberWrapperFactory.CreateAll<T>()) {
                if (m.IsElement) {
                    AddMember(elements, m);
                    if (m.OriginalName == null)
                        _hasEmptyNames = true;
                } else
                    AddMember(_members, m);
            }
            if (_hasEmptyNames && (elements.Count != 1 || !elements[0].MemberType.GetInfo().IsCollection))
                throw new SerializationException(string.Format("Type '{0}' must have only one element, if this element has empty name. Also it's type must be a collection.", typeof(T)));
            _attributeCount = _members.Count;
            _members.AddRange(elements);
        }

        /// <summary>
        /// Добавляет обертку над полем/свойством в список, с учетом порядкового номера.
        /// </summary>
        private static void AddMember(List<TypeMemberWrapper<T>> list, TypeMemberWrapper<T> member) {
            for (int i = 0; i < list.Count; i++) {
                if (list[i].Order > member.Order) {
                    list.Insert(i, member);
                    return;
                }
            }
            list.Add(member);
        }

        /// <summary>
        /// Инициализирует и проверяет список параметров конструктора.
        /// </summary>
        private void InitializeParameters() {
            var objectType = typeof(T);
            var cpInfo = SerializationConfig.ConstructorManager[objectType];
            _parameters = new TypeMemberWrapper<T>[cpInfo.Count];
            foreach (var m in _members)
                if (m.IsConstructorParameter)
                    _parameters[m.ConstructorParameterNumber] = m;
            var notFoundParameter = Array.IndexOf(_parameters, null);
            if (notFoundParameter >= 0)
                throw new SerializationException(string.Format("Cann't find a member '{0}' in type '{1}' for constructor parameters.", cpInfo[notFoundParameter].Name, objectType));
        }

        /// <summary>
        /// Имеются ли в списке параметры конструктора.
        /// </summary>
        public bool HasConstructorParameters {
            get { return _parameters.Length > 0; }
        }
        /// <summary>
        /// Количество параметров конструктора в списке.
        /// </summary>
        public int ConstructorParameterCount {
            get { return _parameters.Length; }
        }

        /// <summary>
        /// Получает все атрибуты в списке.
        /// </summary>
        public IEnumerable<TypeMemberWrapper<T>> GetAttributes() {
            for (int i = 0; i < _attributeCount; i++)
                yield return _members[i];
        }

        /// <summary>
        /// Получает все элементы в списке, прочитанные из контекста.
        /// </summary>
        public IEnumerable<TypeMemberWrapper<T>> GetElements(DeserializationContext context) {
            return _hasEmptyNames ? GetFirstElement() : ReadElements(context);
        }

        /// <summary>
        /// Получает все атрибуты и элементы в списке.
        /// </summary>
        public IEnumerable<TypeMemberWrapper<T>> GetMembers() {
            return _members;
        }

        /// <summary>
        /// Получает все атрибуты и элементы в списке, прочитанные из контекста.
        /// </summary>
        public IEnumerable<TypeMemberWrapper<T>> GetMembers(DeserializationContext context) {
            for (int i = 0; i < _attributeCount; i++)
                yield return _members[i];
            foreach (var m in GetElements(context))
                yield return m;
        }

        /// <summary>
        /// Ищет обертку над полем/свойством по xml-имени элемента.
        /// Возвращает null, если не найдено.
        /// </summary>
        public TypeMemberWrapper<T> FindElement(string name) {
            for (int i = _attributeCount; i < _members.Count; i++) {
                var member = _members[i];
                if (member.Name == name)
                    return member;
            }
            return null;
        }

        /// <summary>
        /// Получает обертку для первого элемента в списке.
        /// </summary>
        private IEnumerable<TypeMemberWrapper<T>> GetFirstElement() {
            if (_attributeCount < _members.Count)
                yield return _members[_attributeCount];
        }

        /// <summary>
        /// Читает элементы из контекста и ищет по ним обертки.
        /// </summary>
        private IEnumerable<TypeMemberWrapper<T>> ReadElements(DeserializationContext context) {
            foreach (var ctx in context.ReadElements(true)) {
                var member = FindElement(ctx.GetName());
                if (member != null)
                    yield return member;
                else if (!ctx.Serializer.IgnoreUnusedElements)
                    throw SerializationException.NotFoundMember(typeof(T), ctx.GetName());
            }
        }

        /// <summary>
        /// Получает список оберток над десериализованными значениями полей/свойств.
        /// </summary>
        public IEnumerable<MemberValue<T>> GetValues(DeserializationContext context) {
            foreach (var m in GetMembers(context))
                yield return m.DeserializeMemberValue(context);
        }

        /// <summary>
        /// Получает список типов параметров конструктора.
        /// </summary>
        public Type[] GetParameterTypes() {
            var types = new Type[_parameters.Length];
            for (int i = 0; i < _parameters.Length; i++)
                types[i] = _parameters[i].MemberType;
            return types;
        }
/*
        #region IEnumerable implementation
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return _members.GetEnumerator();
        }
        #endregion

        #region IEnumerable[Nereal.Serialization.TypeMemberWrapper[Nereal.Serialization.MemberWrapperList.T]] implementation
        IEnumerator<TypeMemberWrapper<T>> IEnumerable<TypeMemberWrapper<T>>.GetEnumerator() {
            return _members.GetEnumerator();
        }
        #endregion
*/
    }
}
