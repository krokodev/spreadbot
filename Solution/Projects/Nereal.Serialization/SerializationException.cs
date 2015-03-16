/*
 *  $Id: SerializationException.cs 194 2010-12-05 12:03:19Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Reflection;

namespace Nereal.Serialization {
    /// <summary>
    /// Основной класс исключений библиотеки Nereal.Serializarion.
    /// </summary>
    public class SerializationException : Exception {
        /// <summary>
        /// Создает новый экземпляр SerializationException без текста исключения.
        /// </summary>
        public SerializationException() : base() {
        }
        /// <summary>
        /// Создает новый экземпляр SerializationException с текстом исключения.
        /// </summary>
        public SerializationException(string message) : base(message) {
        }
        /// <summary>
        /// Создает новый экземпляр SerializationException с текстом исключения по указанному MemberInfo.
        /// </summary>
        public SerializationException(string message, MemberInfo member) : base(string.Format(message, GetFullMemberName(member))) {
        }
        /// <summary>
        /// Создает новый экземпляр SerializationException с текстом исключения и вложенным исключением.
        /// </summary>
        public SerializationException(string message, Exception innerException) : base(message, innerException) {
        }

        /// <summary>
        /// Создает исключение о не найденном члене типа.
        /// </summary>
        public static SerializationException NotFoundMember(Type objectType, string elementName) {
            return new SerializationException(string.Format("Not found member in type '{0}' for element '{1}'.", objectType, elementName));
        }

        /// <summary>
        /// Создает исключение о не реализованном интерфейсе.
        /// </summary>
        public static SerializationException NotImplements(Type objectType, Type interfaceType) {
            return new SerializationException(string.Format("Type '{0}' not implements a '{1}' interface.", objectType, interfaceType));
        }

        /// <summary>
        /// Создает исключение о невозможности строчного списка.
        /// </summary>
        public static SerializationException NotInlineList(MemberWrapper member) {
            return new SerializationException(string.Format("Member '{0}' cannot be stored as a inline list.", member));
        }

        /// <summary>
        /// Проверяет пригодность поля/свойства быть ссылкой, и возвращает исключение об ошибке, если она есть.
        /// </summary>
        internal static SerializationException TestReferenceMember<T>(TypeMemberWrapper<T> member, bool external) {
            if (external) {
                if (member.IsReadOnly && !member.IsConstructorParameter)
                    return new SerializationException("External reference member cannot be read-only and not constructor parameter.");
            } else {
                if (member.IsConstructorParameter)
                    return new SerializationException("Internal reference member cannot be a constructor parameter in current version of library.");
                if (member.IsReadOnly)
                    return new SerializationException("Internal reference member cannot be read-only.");
            }
            return null;
        }

        /// <summary>
        /// Получает полное имя указанного MemberInfo.
        /// </summary>
        internal static string GetFullMemberName(MemberInfo member) {
            return string.Format("'{0}.{1}'", member.ReflectedType.FullName, member.Name);
        }
    }
}
