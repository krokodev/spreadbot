/*
 *  $Id: INullResolver.cs 191 2010-12-02 15:45:28Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

namespace Nereal.Serialization {
    /// <summary>
    /// Интерфейс определителя null-значений.
    /// </summary>
    public interface INullResolver {
        /// <summary>
        /// Проверяет, что текущий элемент в контексте является null.
        /// </summary>
        bool IsNull(DeserializationContext context);
        /// <summary>
        /// Пишет информацию о null в текущий элемент в контексте.
        /// </summary>
        void WriteNull(SerializationContext context);
    }
}
