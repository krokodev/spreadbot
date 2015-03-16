/*
 *  $Id: UpdateableOnlyAttribute.cs 157 2010-11-04 12:13:12Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Указывает, что данный класс только обновляемый, и на него не нужно генерировать конструктор.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class UpdateableOnlyAttribute : Attribute {
    }
}
