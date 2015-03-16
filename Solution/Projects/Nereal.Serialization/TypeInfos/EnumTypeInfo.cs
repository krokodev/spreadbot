/*
 *  $Id: EnumTypeInfo.cs 169 2010-11-17 15:45:13Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

using Nereal.Extensions;

namespace Nereal.Serialization.TypeInfos {
    /// <summary>
    /// Информация о енуме (перечислимом типе).
    /// </summary>
    public class EnumTypeInfo<T> : ValueTypeInfo<T> where T : struct {
        private bool _isFlags;

        /// <summary>
        /// Создает новый экземпляр информации о енуме.
        /// </summary>
        public EnumTypeInfo() {
            _isFlags = ThisType.HasAttribute<FlagsAttribute>();
        }

        /// <summary>
        /// Енум может быть сохранен в строчном списке только если он не множественный.
        /// </summary>
        public override bool CanInlineToList {
            get { return !_isFlags; }
        }

        /// <summary>
        /// Конвертирует строку в енум путем вызова Enum.Parse.
        /// </summary>
        public override T ConvertFromString(string value) {
            return (T) Enum.Parse(ThisType, value, true);
        }
    }

    /// <summary>
    /// Селектор класса информации о енумах.
    /// </summary>
    [TypeInfoSelector]
    public class EnumTypeInfoSelector : ITypeInfoSelector {
        /// <summary>
        /// Второй приоритет (значимые типы).
        /// </summary>
        public int Priority {
            get { return 10; }
        }

        /// <summary>
        /// Проверяет, что тип является енумом.
        /// </summary>
        public bool Accept(Type objectType) {
            return objectType.IsValueType && objectType.IsEnum;
        }

        /// <summary>
        /// Получает класс информации о енуме.
        /// </summary>
        public Type GetTypeInfo(Type objectType) {
            return typeof(EnumTypeInfo<>).MakeGenericType(objectType);
        }
    }
}
