/*
 *  $Id: XmlInlineListAttribute.cs 129 2010-10-25 12:54:47Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Указывает, что массив или список могут храниться в виде одной строки с пробелом между значениями.
    /// Применимо только если тип значений это позволяет.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class XmlInlineListAttribute : Attribute {
    }
}
