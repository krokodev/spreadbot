/*
 *  $Id: TypeInfoSelectorAttribute.cs 144 2010-10-28 12:11:08Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Указывает, что данный класс является селектором для <see cref="T:TypeInfo`1"/>.
    /// Класс должен реализовывать интерфейс <see cref="ITypeInfoSelector" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TypeInfoSelectorAttribute : Attribute {
    }
}

