/*
 *  $Id: Tuple.cs 144 2010-10-28 12:11:08Z thenn $
 *  This file is a part of Nereal Extensions library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;

namespace Nereal.Extensions {
    /// <summary>
    /// Класс с методами для создания Tuple.
    /// </summary>
    public static class Tuple {
        /// <summary>
        /// Создает Tuple с двумя значениями.
        /// </summary>
        public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2) {
            return new Tuple<T1, T2> { Item1 = item1, Item2 = item2 };
        }
    }

    /// <summary>
    /// Структура с двумя произвольными значениями.
    /// TODO : В .NET4 можно использовать стандартную структуру.
    /// </summary>
    public struct Tuple<T1, T2> {
        /// <summary>
        /// Первое значение.
        /// </summary>
        public T1 Item1;
        /// <summary>
        /// Второе значение.
        /// </summary>
        public T2 Item2;

        /// <summary>
        /// Получает общий хеш-код на оба значения.
        /// </summary>
        public override int GetHashCode() {
            return Item1.GetHashCode() * 7 ^ Item2.GetHashCode();
        }

        /// <summary>
        /// Проверяет на равенство по обоим значениям.
        /// </summary>
        public override bool Equals(object obj) {
            if (!(obj is Tuple<T1, T2>))
                return false;
            var other = (Tuple<T1, T2>) obj;
            return this.Item1.Equals(other.Item1) && this.Item2.Equals(other.Item2);
        }
    }
}
