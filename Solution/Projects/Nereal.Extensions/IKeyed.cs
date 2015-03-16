/*
 *  $Id: IKeyed.cs 105 2010-10-22 10:25:36Z thenn $
 *  This file is a part of Nereal Extensions library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Extensions {
    /// <summary>
    /// Базовый интерфейс для объектов, имеющих ключ-идентификатор.
    /// </summary>
    public interface IKeyed<T> {
        /// <summary>
        /// Ключ-идентификатор объекта.
        /// </summary>
        T Key { get; }
    }
}

