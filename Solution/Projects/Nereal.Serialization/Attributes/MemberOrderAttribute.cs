/*
 *  $Id: MemberOrderAttribute.cs 163 2010-11-14 16:49:56Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization {
    /// <summary>
    /// Указывает полям и свойствам порядковый номер.
    /// Все поля/свойства, имеющие этот атрибут, будут (де)сериализованы именно в данном порядке, а только затем все оставшиеся, не имеющие атрибута (они считаются как Order=int.MaxValue).
    /// Номера для xml-атрибутов и xml-элементов раздельны, в любом случае сначала идут все атрибуты, потом все элементы.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class MemberOrderAttribute : Attribute {
        /// <summary>
        /// Порядковый номер поля/свойства.
        /// </summary>
        public readonly int Order;

        /// <summary>
        /// Создает новый экземпляр MemberOrderAttribute.
        /// </summary>
        public MemberOrderAttribute(int order) {
            Order = order;
        }
    }
}

