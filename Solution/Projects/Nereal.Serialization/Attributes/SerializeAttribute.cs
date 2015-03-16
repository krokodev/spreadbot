/*
 *  $Id: SerializeAttribute.cs 180 2010-11-26 09:25:29Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Указывает, что данное приватное/защищенное поле/свойство является сериализуемым.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class SerializeAttribute : Attribute {
    }
}
