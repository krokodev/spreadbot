/*
 *  $Id: IKeyResolver.cs 182 2010-11-28 12:24:42Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

namespace Nereal.Serialization {
    /// <summary>
    /// Интерфейс для разрешения значений в ключи-идентификаторы и обратно.
    /// </summary>
    public interface IKeyResolver<TKey, TValue> {
        /// <summary>
        /// Разрешает и получает ключ по значению.
        /// </summary>
        TKey ResolveKey(TValue value);

        /// <summary>
        /// Разрешает и получает значение по ключу.
        /// </summary>
        TValue ResolveValue(TKey key);
    }
}
