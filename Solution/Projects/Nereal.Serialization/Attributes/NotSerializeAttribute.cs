/*
 *  $Id: NotSerializeAttribute.cs 153 2010-11-03 14:53:39Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Указывает, что данное поле/свойство не является сериализуемым.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class NotSerializeAttribute : Attribute {
    }
}

