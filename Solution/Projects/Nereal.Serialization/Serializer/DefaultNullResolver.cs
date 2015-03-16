/*
 *  $Id: DefaultNullResolver.cs 191 2010-12-02 15:45:28Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

namespace Nereal.Serialization {
    /// <summary>
    /// Стандартный определитель null-значений.
    /// </summary>
    public sealed class DefaultNullResolver : INullResolver {
        /// <summary>
        /// Имя xml-атрибута, сообщающего о null-значении.
        /// </summary>
        public const string AttributeName = "null";

        /// <summary>
        /// Информация о типе bool.
        /// </summary>
        private static readonly TypeInfo<bool> BoolInfo = SerializationConfig.InfoManager.GetInfo<bool>();

        /// <summary>
        /// Проверяет, что текущий элемент в контексте является null.
        /// </summary>
        public bool IsNull(DeserializationContext context) {
            return BoolInfo.ConvertFromString(context.ReadAttribute(AttributeName));
        }

        /// <summary>
        /// Пишет информацию о null в текущий элемент в контексте.
        /// </summary>
        public void WriteNull(SerializationContext context) {
            context.WriteAttribute(AttributeName, BoolInfo.ConvertToString(true));
        }
    }
}
