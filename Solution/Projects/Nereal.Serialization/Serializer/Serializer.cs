/*
 *  $Id: Serializer.cs 199 2011-03-23 11:37:26Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace Nereal.Serialization {
    /// <summary>
    /// Основной класс сериализатора.
    /// </summary>
    public sealed class Serializer {
        /// <summary>
        /// Стандартный экземпляр сериализатора. Можно использовать, если не требуется никаких отдельных настроек.
        /// </summary>
        public static readonly Serializer Default = new Serializer();

        private bool _insertXmlDeclaration, _indentOutput, _ignoreUnusedElements;
        private INullResolver _nullResolver;
        private KeyResolverList _resolvers;
        private ReferenceCollectorList _collectors;

        /// <summary>
        /// Создает новый экземпляр сериализатора.
        /// </summary>
        public Serializer() {
            _insertXmlDeclaration = true;
            _indentOutput = true;
            _ignoreUnusedElements = false;
            _nullResolver = new DefaultNullResolver();
            _resolvers = new KeyResolverList();
            _collectors = new ReferenceCollectorList();
        }

        /// <summary>
        /// Вставлять xml-declaration в начале сериализации.
        /// </summary>
        public bool InsertXmlDeclaration {
            get { return _insertXmlDeclaration; }
            set { _insertXmlDeclaration = value; }
        }

        /// <summary>
        /// Добавлять отступы к xml-элементам.
        /// </summary>
        public bool IndentOutput {
            get { return _indentOutput; }
            set { _indentOutput = value; }
        }

        /// <summary>
        /// Игнорировать элементы, не использованные при десериализации.
        /// </summary>
        public bool IgnoreUnusedElements {
            get { return _ignoreUnusedElements; }
            set { _ignoreUnusedElements = value; }
        }

        /// <summary>
        /// Определитель null-значений.
        /// </summary>
        public INullResolver NullResolver {
            get { return _nullResolver; }
            set { _nullResolver = value ?? new DefaultNullResolver(); }
        }

        /// <summary>
        /// Список определителейм ключей.
        /// </summary>
        public KeyResolverList Resolvers {
            get { return _resolvers; }
        }

        /// <summary>
        /// Список коллекторов ссылок.
        /// </summary>
        public ReferenceCollectorList Collectors {
            get { return _collectors; }
        }

        /// <summary>
        /// Обрабатывает указанный тип, подготовляя всю необходимую информацию для сериализации.
        /// </summary>
        public static void PrepareType(Type type) {
            var info = type.GetInfo();
            info.EnsureInitialize();
            SerializationConfig.ResolverManager.Prepare(type);
        }
        /// <summary>
        /// Обрабатывает указанные типы, подготовляя всю необходимую информацию для сериализации.
        /// </summary>
        public static void PrepareTypes(params Type[] types) {
            PrepareTypes(types as IEnumerable<Type>);
        }
        /// <summary>
        /// Обрабатывает указанные типы, подготовляя всю необходимую информацию для сериализации.
        /// </summary>
        public static void PrepareTypes(IEnumerable<Type> types) {
            foreach (var type in types)
                PrepareType(type);
        }

        /// <summary>
        /// Десериализует объект из указанного <see cref="TextReader" />.
        /// </summary>
        public T Deserialize<T>(TextReader reader, bool ownedReader) {
            try {
                var context = new DeserializationContext(this, reader);
                var value = context.ReadElement<T>(typeof(T).GetInfo().RootWrapper);
                context.Close();
                return value;
            } finally {
                if (ownedReader)
                    reader.Dispose();
            }
        }

        /// <summary>
        /// Десериализует объект из указанного <see cref="Stream" />.
        /// </summary>
        public T Deserialize<T>(Stream stream, bool ownedStream) {
            var reader = new StreamReader(stream);
            return Deserialize<T>(reader, ownedStream);
        }

        /// <summary>
        /// Десериализует объект из файла с указанным именем.
        /// </summary>
        public T Deserialize<T>(string filename) {
            var reader = new StreamReader(filename);
            return Deserialize<T>(reader, true);
        }

        /// <summary>
        /// Десериализует существующий объект из указанного <see cref="TextReader" />.
        /// </summary>
        public void Deserialize<T>(T value, TextReader reader) {
            var context = new DeserializationContext(this, reader);
            context.ReadElement<T>(typeof(T).GetInfo().RootWrapper, value);
            context.Close();
        }

        /// <summary>
        /// Десериализует существующий объект из указанного <see cref="Stream" />.
        /// </summary>
        public void Deserialize<T>(T value, Stream stream) {
            using (var reader = new StreamReader(stream))
                Deserialize(value, reader);
        }

        /// <summary>
        /// Десериализует существующий объект из файла с указанным именем.
        /// </summary>
        public void Deserialize<T>(T value, string filename) {
            using (var reader = new StreamReader(filename))
                Deserialize(value, reader);
        }

        /// <summary>
        /// Сериализует объект в указанный <see cref="TextWriter" />.
        /// </summary>
        public void Serialize<T>(T value, TextWriter writer, bool ownedWriter) {
            try {
                var context = new SerializationContext(this, writer);
                context.Write(typeof(T).GetInfo().RootWrapper, value);
                context.Close();
            } finally {
                if (ownedWriter)
                    writer.Dispose();
            }
        }

        /// <summary>
        /// Сериализует объект в указанный <see cref="Stream" />.
        /// </summary>
        public void Serialize<T>(T value, Stream stream, bool ownedStream) {
            var writer = new StreamWriter(stream);
            Serialize(value, writer, ownedStream);
        }

        /// <summary>
        /// Сериализует объект в файл с указанным именем.
        /// </summary>
        public void Serialize<T>(T value, string filename) {
            var writer = new StreamWriter(filename);
            Serialize(value, writer, true);
        }
    }
}
