/*
 *  $Id: ByteArrayTypeInfo.cs 130 2010-10-26 10:56:08Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization.TypeInfos {
    /// <summary>
    /// Информация о байтовом массиве.
    /// </summary>
    [TypeInfo(typeof(byte[]))]
    public class ByteArrayTypeInfo : ArrayTypeInfo<byte> {
        /// <summary>
        /// Байтовый массив может быть сохранен в строчном списке.
        /// </summary>
        public override bool CanInlineToList {
            get { return true; }
        }

        /// <summary>
        /// Байтовый массив может быть сохранен в виде атрибута.
        /// </summary>
        public override bool IsElement(MemberWrapper member) {
            return false;
        }

        /// <summary>
        /// Клонирует байтовый массив.
        /// </summary>
        public override byte[] Clone(byte[] value) {
            var newValue = new byte[value.Length];
            value.CopyTo(newValue, 0);
            return newValue;
        }

        /// <summary>
        /// Десериализует байтовый массив путем конвертации из строки.
        /// </summary>
        public override byte[] Deserialize(MemberWrapper member, DeserializationContext context) {
            return ConvertFromString(context.ReadText());
        }

        /// <summary>
        /// Сериализует байтовый массив путем конвертации в строку.
        /// </summary>
        public override void Serialize(MemberWrapper member, SerializationContext context, byte[] value) {
            context.WriteText(ConvertToString(value));
        }

        /// <summary>
        /// Конвертирует base64-строку в байтовый массив.
        /// </summary>
        public override byte[] ConvertFromString(string value) {
            return Convert.FromBase64String(value);
        }

        /// <summary>
        /// Конвертирует байтовый массив в base64-строку.
        /// </summary>
        public override string ConvertToString(byte[] value) {
            return Convert.ToBase64String(value);
        }
    }
}
