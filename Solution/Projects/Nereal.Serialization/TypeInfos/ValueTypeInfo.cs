/*
 *  $Id: ValueTypeInfo.cs 190 2010-12-02 13:20:50Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Serialization.TypeInfos {
    /// <summary>
    /// Информация о стандартных значимых типах.
    /// Структуры сюда не относятся.
    /// </summary>
    public class ValueTypeInfo<T> : TypeInfo<T> where T : struct {
        /// <summary>
        /// Значимые типы не могут иметь потомков.
        /// </summary>
        public override bool IsSealed {
            get { return true; }
        }

        /// <summary>
        /// Значимый тип может быть сохранен в строчном списке.
        /// </summary>
        public override bool CanInlineToList {
            get { return true; }
        }

        /// <summary>
        /// Возвращает само же значение, вместо клонирования.
        /// </summary>
        public override T Clone(T value) {
            return value;
        }

        /// <summary>
        /// Сравнивает два значения.
        /// </summary>
        public override bool Equals(T a, T b) {
            return a.Equals(b);
        }

        /// <summary>
        /// Проверяет необходимость сериализации значения по неравенству значению по умолчанию.
        /// </summary>
        public override bool NeedSerialize(T value, bool hasDefaultValue, T defaultValue) {
            return !hasDefaultValue || !Equals(value, defaultValue);
        }
    }

    /// <summary>
    /// Селектор класса информации о значимых типах.
    /// </summary>
    [TypeInfoSelector]
    public class ValueTypeInfoSelector : ITypeInfoSelector {
        /// <summary>
        /// Второй приоритет (значимые типы).
        /// </summary>
        public int Priority {
            get { return 10; }
        }

        /// <summary>
        /// Проверяет, что тип является значимым, но при это не енум и не структура.
        /// </summary>
        public bool Accept(Type objectType) {
            return objectType.IsValueType && !objectType.IsEnum && Type.GetTypeCode(objectType) != TypeCode.Object;
        }

        /// <summary>
        /// Получает класс информации о значимом типе.
        /// </summary>
        public Type GetTypeInfo(Type objectType) {
            return typeof(ValueTypeInfo<>).MakeGenericType(objectType);
        }
    }
}
