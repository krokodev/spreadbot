/*
 *  $Id: KeyedValueAttribute.cs 144 2010-10-28 12:11:08Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Указывает, что значения словаря реализуют интерфейс <see cref="T:Nereal.Extensions.IKeyed`2"/>, и для них не нужно отдельно сохранять ключ.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class KeyedValueAttribute : Attribute {
    }
}
