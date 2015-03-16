/*
 *  $Id: XmlStoreAsElementAttribute.cs 129 2010-10-25 12:54:47Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Указывает, что поле или свойство сохраняется в xml-элемент.
    /// При отсутствии данного указания это определяется автоматически.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class XmlStoreAsElementAttribute : Attribute {
    }
}
