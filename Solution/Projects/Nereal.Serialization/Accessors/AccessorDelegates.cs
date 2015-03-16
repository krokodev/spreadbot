/*
 *  $Id: AccessorDelegates.cs 144 2010-10-28 12:11:08Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

namespace Nereal.Serialization {
    /// <summary>
    /// Делегат получения значения.
    /// </summary>
    public delegate V GetterDelegate<T, V>(T obj);

    /// <summary>
    /// Делегат установки значения.
    /// </summary>
    public delegate void SetterDelegate<T, V>(T obj, V value);

    /// <summary>
    /// Делегат получения значения с передачей объекта по ссылке.
    /// </summary>
    public delegate V RefGetterDelegate<T, V>(ref T obj);

    /// <summary>
    /// Делегат установки значения с передачей объекта по ссылке.
    /// </summary>
    public delegate void RefSetterDelegate<T, V>(ref T obj, V value);

    /// <summary>
    /// Делегат конструирования объекта с параметрами.
    /// </summary>
    public delegate T ParametrizedConstructorDelegate<T>(object[] parameters);

    /// <summary>
    /// Делегат конструирования объекта без параметров.
    /// </summary>
    public delegate T ConstructorDelegate<T>() where T : class;
}
