/*
 *  $Id: SerializationContext.cs 191 2010-12-02 15:45:28Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Nereal.Serialization {
    /// <summary>
    /// Контекст сериализации.
    /// Инкапсулирует непосредственную работу с <see cref="System.Xml.XmlWriter" />.
    /// </summary>
    public sealed class SerializationContext {
        private Serializer _serializer;
        private XmlTextWriter _writer;

        internal SerializationContext(Serializer serializer, TextWriter writer) {
            _serializer = serializer;
            _writer = new XmlTextWriter(writer);
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
            if (_serializer.IndentOutput) {
                _writer.Formatting = Formatting.Indented;
                _writer.Indentation = 2;
                _writer.IndentChar = ' ';
            } else {
                _writer.Formatting = Formatting.None;
            }
            if (_serializer.InsertXmlDeclaration)
                _writer.WriteStartDocument();
        }

        /// <summary>
        /// Закрывает контекст.
        /// </summary>
        public void Close() {
            if (_serializer.InsertXmlDeclaration)
                _writer.WriteEndDocument();
            _writer.Flush();
        }

        /// <summary>
        /// Начинает новый элемент с указанным именем.
        /// </summary>
        public void StartElement(string name) {
            _writer.WriteStartElement(name);
        }
        /// <summary>
        /// Завершает элемент.
        /// </summary>
        public void EndElement() {
            _writer.WriteEndElement();
        }

        /// <summary>
        /// Пишет в поток текстовое значение.
        /// </summary>
        public void WriteText(string value) {
            _writer.WriteString(value);
        }

        /// <summary>
        /// Пишет в поток атрибут с указанными именем и значением.
        /// </summary>
        public void WriteAttribute(string name, string value) {
            _writer.WriteAttributeString(name, value);
        }

        /// <summary>
        /// Пишет в поток элемент с указанными именем и текстовым значением.
        /// </summary>
        public void WriteElement(string name, string value) {
            _writer.WriteElementString(name, value);
        }

        /// <summary>
        /// Получает имя элемента по обертке поля/свойства и реальному типу объекта.
        /// </summary>
        public string GetElementName(MemberWrapper member, Type objectType) {
            if (!member.MemberType.IsSealed) {
                var resolver = member.MemberType.GetResolver();
                var typedName = resolver.GetTypeElementName(objectType);
                if (!string.IsNullOrEmpty(typedName))
                    return typedName;
            }
            return member.Name;
        }

        /// <summary>
        /// Пишет в поток тип объекта, если это необходимо.
        /// </summary>
        public void WriteType(MemberWrapper member, Type objectType) {
            if (!member.MemberType.IsSealed) {
                var resolver = member.MemberType.GetResolver();
                resolver.Serialize(this, member.MemberType, objectType);
            }
        }

        /// <summary>
        /// Пишет в поток объект по указанной обертке поля/свойства.
        /// </summary>
        public void Write<T>(MemberWrapper member, T value) {
            if (member.IsElement) {
                var info = SerializationConfig.InfoManager.GetInfo<T>();
                if (info.IsNull(value)) {
                    WriteNull(member);
                    return;
                }
                var objectType = value.GetType();
                if (objectType != info.ThisType)
                    info = objectType.GetInfo<T>();
                var hasGroupTag = !(info.IsCollection && member.OriginalName == null);
                if (hasGroupTag) {
                    StartElement(GetElementName(member, objectType));
                    WriteType(member, objectType);
                }
                info.Serialize(member, this, value);
                if (hasGroupTag)
                    EndElement();
            } else {
                var info = SerializationConfig.InfoManager.GetInfo<T>();
                WriteAttribute(member.Name, info.ConvertToString(value));
            }
        }

        /// <summary>
        /// Пишет в поток список объектов по указанной обертке поля/свойства.
        /// </summary>
        public void WriteElements<T>(MemberWrapper member, IEnumerable<T> values) {
            foreach (var value in values)
                Write(member, value);
        }

        /// <summary>
        /// Пишет в поток элемент с null-значением.
        /// </summary>
        private void WriteNull(MemberWrapper member) {
            StartElement(member.Name);
            _serializer.NullResolver.WriteNull(this);
            EndElement();
        }
    }
}
