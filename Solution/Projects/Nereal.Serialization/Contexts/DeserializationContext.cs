/*
 *  $Id: DeserializationContext.cs 191 2010-12-02 15:45:28Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Контекст десериализации.
    /// Инкапсулирует непосредственную работу с <see cref="System.Xml.XmlReader" />, содержит список коллекторов ссылок.
    /// </summary>
    public sealed class DeserializationContext {
        private Serializer _serializer;
        private XmlReader _reader;
        private Stack<XmlReader> _prev;

        internal DeserializationContext(Serializer serializer, TextReader reader) {
            _serializer = serializer;
            _reader = XmlReader.Create(reader, new XmlReaderSettings { IgnoreComments = true, IgnoreProcessingInstructions = true, IgnoreWhitespace = true });
            _prev = new Stack<XmlReader>();
            StartDocument();
        }

        /// <summary>
        /// Экземпляр сериализатора, из которого вызван этот контекст.
        /// </summary>
        public Serializer Serializer {
            get { return _serializer; }
        }

        /// <summary>
        /// Обрабатывает начало документа.
        /// </summary>
        private void StartDocument() {
            SkipToElement(true);
        }

        /// <summary>
        /// Начинает чтение поддерева элементов.
        /// </summary>
        private void StartSubtree() {
            _prev.Push(_reader);
            _reader = _reader.ReadSubtree();
            StartDocument();
        }

        /// <summary>
        /// Закрывает контекст на один уровень чтения.
        /// </summary>
        public void Close() {
            _reader.Close();
            _reader = _prev.Count > 0 ? _prev.Pop() : null;
        }

        /// <summary>
        /// Пропускает в потоке чтения всё до начала следующего элемента.
        /// </summary>
        /// <param name="skipCurrent">Если true, то пропускает текущий узел.</param>
        /// <returns>True, если элемент найден.</returns>
        private bool SkipToElement(bool skipCurrent) {
            if (skipCurrent) {
                while (_reader.Read())
                    if (_reader.NodeType == XmlNodeType.Element)
                        return true;
            } else {
                do {
                    if (_reader.NodeType == XmlNodeType.Element)
                        return true;
                } while (_reader.Read());
            }
            return false;
        }

        /// <summary>
        /// Получает имя текущего элемента.
        /// </summary>
        public string GetName() {
            return _reader.Name;
        }

        /// <summary>
        /// Читает текстовое содержимое текущего элемента.
        /// </summary>
        public string ReadText() {
            return _reader.ReadString();
        }

        /// <summary>
        /// Проверяет наличие атрибута с указанным именем.
        /// </summary>
        public bool HasAttribute(string name) {
            return _reader.GetAttribute(name) != null;
        }

        /// <summary>
        /// Читает значение атрибута с указанным именем. При его отсутствии возвращает пустую строку.
        /// </summary>
        public string ReadAttribute(string name) {
            return ReadAttribute(name, string.Empty);
        }
        /// <summary>
        /// Читает значение атрибута с указанным именем. При его отсутствии возвращает указанное значение по умолчанию.
        /// </summary>
        public string ReadAttribute(string name, string defValue) {
            return _reader.GetAttribute(name) ?? defValue;
        }

        /// <summary>
        /// Читает тип объекта в текущем элементе.
        /// </summary>
        public Type ReadType<T>() {
            return ReadType(typeof(T));
        }
        /// <summary>
        /// Читает тип объекта в текущем элементе.
        /// </summary>
        public Type ReadType(Type baseType) {
            var objectType = baseType;
            if (!baseType.IsSealed) {
                var resolver = SerializationConfig.ResolverManager[baseType];
                objectType = resolver.Deserialize(this, baseType) ?? baseType;
            }
            return objectType;
        }

        /// <summary>
        /// Читает текущий элемент по указанной обертке поля/свойства.
        /// Если указано, проверяет вид сохранения и имя обертки, если она не элемент и её имя не равно имени текущего элемента, то выдает исключение.
        /// </summary>
        public T ReadElement<T>(MemberWrapper member) {
            TestElement(member);
            if (_serializer.NullResolver.IsNull(this))
                return default(T);
            var objectType = ReadType(member.MemberType);
            var info = objectType.GetInfo<T>();
            return info.Deserialize(member, this);
        }

        /// <summary>
        /// Читает текущий элемент по указанной обертке поля/свойства в существующий объект.
        /// Если указано, проверяет вид сохранения и имя обертки, если она не элемент и её имя не равно имени текущего элемента, то выдает исключение.
        /// </summary>
        public void ReadElement<T>(MemberWrapper member, T obj) {
            TestElement(member);
            var info = obj.GetType().GetInfo<T>();
            info.Deserialize(member, this, obj);
        }

        /// <summary>
        /// Проверяет вид сохранения и имя обертки, если она не элемент и её имя не равно имени текущего элемента, то выдает исключение.
        /// </summary>
        private void TestElement(MemberWrapper member) {
            if (!member.IsElement)
                throw new SerializationException(string.Format("Cann't read '{0}' attribute by ReadElement method.", member.Name));
            if (member.OriginalName != null && member.OriginalName != GetName())
                throw new SerializationException(string.Format("Element '{0}' found, but '{1}' expected.", GetName(), member.Name));
        }

        /// <summary>
        /// Читает все элементы на текущем или вложенном уровне, и выдает список контекстов для чтения поддеревьев.
        /// Контексты активны только до следующего шага итерации по списку, поэтому их нельзя сохранять, с ними нужно работать только внутри цикла итерации по списку.
        /// </summary>
        /// <param name="childs">Если true, читаются вложенные в текущий элементы.</param>
        public IEnumerable<DeserializationContext> ReadElements(bool childs) {
            if (SkipToElement(false)) {
                if (!childs || (!_reader.IsEmptyElement && _reader.Read())) {
                    do {
                        StartSubtree();
                        yield return this;
                        Close();
                    } while (SkipToElement(true));
                }
            }
        }

        /// <summary>
        /// Читает все вложенные в текущий элементы как объекты указанного типа и по указанной обертке поля/свойства.
        /// </summary>
        public IEnumerable<T> ReadElements<T>(MemberWrapper member) {
            foreach (var ctx in ReadElements(true))
                yield return ctx.ReadElement<T>(member);
        }
    }
}
