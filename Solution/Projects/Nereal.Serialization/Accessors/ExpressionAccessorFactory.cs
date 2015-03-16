/*
 *  $Id: ExpressionAccessorFactory.cs 144 2010-10-28 12:11:08Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Nereal.Serialization {
    /// <summary>
    /// Реализация фабрики делегатов доступа с помощью Linq Expressions.
    /// TODO : До .NET4 невозможна установка полей, потому на данный момент реализация не полная.
    /// </summary>
    public sealed class ExpressionAccessorFactory : IAccessorFactory {
        /// <summary>
        /// Создает делегат получения значения поля.
        /// </summary>
        public GetterDelegate<T, V> CreateGetter<T, V>(FieldInfo field) {
            var obj = Expression.Parameter(typeof(T), "obj");
            var getter = Expression.Field(obj, field);
            var expr = Expression.Lambda<GetterDelegate<T, V>>(getter, obj);
            return expr.Compile();
        }

        /// <summary>
        /// Создает делегат получения значения свойства.
        /// </summary>
        public GetterDelegate<T, V> CreateGetter<T, V>(PropertyInfo property) {
            var obj = Expression.Parameter(typeof(T), "obj");
            var getter = Expression.Call(obj, property.GetGetMethod());
            var expr = Expression.Lambda<GetterDelegate<T, V>>(getter, obj);
            return expr.Compile();
        }

        /// <summary>
        /// Создает делегат получения значения поля с передачей объекта по ссылке.
        /// </summary>
        public RefGetterDelegate<T, V> CreateRefGetter<T, V>(FieldInfo field) {
            var obj = Expression.Parameter(typeof(T).MakeByRefType(), "obj");
            var getter = Expression.Field(obj, field);
            var expr = Expression.Lambda<RefGetterDelegate<T, V>>(getter, obj);
            return expr.Compile();
        }

        /// <summary>
        /// Создает делегат получения значения свойства с передачей объекта по ссылке.
        /// </summary>
        public RefGetterDelegate<T, V> CreateRefGetter<T, V>(PropertyInfo property) {
            var obj = Expression.Parameter(typeof(T).MakeByRefType(), "obj");
            var getter = Expression.Call(obj, property.GetGetMethod());
            var expr = Expression.Lambda<RefGetterDelegate<T, V>>(getter, obj);
            return expr.Compile();
        }

        /// <summary>
        /// Создает делегат установки значения поля.
        /// </summary>
        public SetterDelegate<T, V> CreateSetter<T, V>(FieldInfo field) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Создает делегат установки значения свойства.
        /// FIXME : На данный момент не реализовано.
        /// </summary>
        public SetterDelegate<T, V> CreateSetter<T, V>(PropertyInfo property) {
            var obj = Expression.Parameter(typeof(T), "obj");
            var value = Expression.Parameter(typeof(V), "value");
            var setter = Expression.Call(obj, property.GetSetMethod(), value);
            var expr = Expression.Lambda<SetterDelegate<T, V>>(setter, obj, value);
            return expr.Compile();
        }

        /// <summary>
        /// Создает делегат установки значения поля с передачей объекта по ссылке.
        /// FIXME : На данный момент не реализовано.
        /// </summary>
        public RefSetterDelegate<T, V> CreateRefSetter<T, V>(FieldInfo field) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Создает делегат установки значения свойства с передачей объекта по ссылке.
        /// </summary>
        public RefSetterDelegate<T, V> CreateRefSetter<T, V>(PropertyInfo property) {
            var obj = Expression.Parameter(typeof(T).MakeByRefType(), "obj");
            var value = Expression.Parameter(typeof(V), "value");
            var setter = Expression.Call(obj, property.GetSetMethod(), value);
            var expr = Expression.Lambda<RefSetterDelegate<T, V>>(setter, obj, value);
            return expr.Compile();
        }

        /// <summary>
        /// Создает делегат конструирования объекта без параметров.
        /// </summary>
        public ConstructorDelegate<T> CreateConstructor<T>() where T : class {
            var obj = Expression.New(typeof(T));
            var expr = Expression.Lambda<ConstructorDelegate<T>>(obj);
            return expr.Compile();
        }

        /// <summary>
        /// Создает делегат конструирования объекта с параметрами указанных типов.
        /// FIXME : На данный момент не реализовано.
        /// </summary>
        public ParametrizedConstructorDelegate<T> CreateConstructor<T>(Type[] parameterTypes) {
            throw new NotImplementedException();
        }
    }
}

