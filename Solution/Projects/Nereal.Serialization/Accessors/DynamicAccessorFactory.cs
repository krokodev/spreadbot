/*
 *  $Id: DynamicAccessorFactory.cs 180 2010-11-26 09:25:29Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Реализация фабрики делегатов доступа с помощью DynamicMethod.
    /// </summary>
    public sealed class DynamicAccessorFactory : IAccessorFactory {
        /// <summary>
        /// Создает делегат получения значения поля.
        /// </summary>
        public GetterDelegate<T, V> CreateGetter<T, V>(FieldInfo field) {
            var dm = CreateGetter(field, typeof(T), typeof(V));
            return CreateDelegate<GetterDelegate<T, V>>(dm);
        }

        /// <summary>
        /// Создает делегат получения значения свойства.
        /// </summary>
        public GetterDelegate<T, V> CreateGetter<T, V>(PropertyInfo property) {
            var dm = CreateGetter(property, typeof(T), typeof(V));
            return CreateDelegate<GetterDelegate<T, V>>(dm);
        }

        /// <summary>
        /// Создает делегат получения значения поля с передачей объекта по ссылке.
        /// </summary>
        public RefGetterDelegate<T, V> CreateRefGetter<T, V>(FieldInfo field) {
            var dm = CreateGetter(field, typeof(T).MakeByRefType(), typeof(V));
            return CreateDelegate<RefGetterDelegate<T, V>>(dm);
        }

        /// <summary>
        /// Создает делегат получения значения свойства с передачей объекта по ссылке.
        /// </summary>
        public RefGetterDelegate<T, V> CreateRefGetter<T, V>(PropertyInfo property) {
            var dm = CreateGetter(property, typeof(T).MakeByRefType(), typeof(V));
            return CreateDelegate<RefGetterDelegate<T, V>>(dm);
        }

        /// <summary>
        /// Создает динамик-метод получения значения поля по типу объекта и типу значения.
        /// </summary>
        private DynamicMethod CreateGetter(FieldInfo field, Type paramType, Type resultType) {
            var dm = new DynamicMethod("getter_" + field.Name, resultType, new Type[] { paramType }, field.DeclaringType);
            var gen = dm.GetILGenerator();
            if (paramType.IsValueType)
                gen.Emit(OpCodes.Ldarga, 0);
            else
                gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldfld, field);
            gen.Emit(OpCodes.Ret);
            return dm;
        }

        /// <summary>
        /// Создает динамик-метод получения значения свойства по типу объекта и типу значения.
        /// </summary>
        private DynamicMethod CreateGetter(PropertyInfo property, Type paramType, Type resultType) {
            var paramTypes = new Type[] { paramType };
            var dm = new DynamicMethod("getter_" + property.Name, resultType, paramTypes, property.DeclaringType);
            var gen = dm.GetILGenerator();
            gen.Emit(OpCodes.Ldarg_0);
            var method = property.GetGetMethod(true);
            gen.EmitCall(method.IsVirtual ? OpCodes.Callvirt : OpCodes.Call, method, null);
            gen.Emit(OpCodes.Ret);
            return dm;
        }

        /// <summary>
        /// Создает делегат установки значения поля.
        /// </summary>
        public SetterDelegate<T, V> CreateSetter<T, V>(FieldInfo field) {
            var dm = CreateSetter(field, typeof(T), typeof(V));
            return CreateDelegate<SetterDelegate<T, V>>(dm);
        }

        /// <summary>
        /// Создает делегат установки значения свойства.
        /// </summary>
        public SetterDelegate<T, V> CreateSetter<T, V>(PropertyInfo property) {
            var dm = CreateSetter(property, typeof(T), typeof(V));
            return CreateDelegate<SetterDelegate<T, V>>(dm);
        }

        /// <summary>
        /// Создает делегат установки значения поля с передачей объекта по ссылке.
        /// </summary>
        public RefSetterDelegate<T, V> CreateRefSetter<T, V>(FieldInfo field) {
            var dm = CreateSetter(field, typeof(T).MakeByRefType(), typeof(V));
            return CreateDelegate<RefSetterDelegate<T, V>>(dm);
        }

        /// <summary>
        /// Создает делегат установки значения свойства с передачей объекта по ссылке.
        /// </summary>
        public RefSetterDelegate<T, V> CreateRefSetter<T, V>(PropertyInfo property) {
            var dm = CreateSetter(property, typeof(T).MakeByRefType(), typeof(V));
            return CreateDelegate<RefSetterDelegate<T, V>>(dm);
        }

        /// <summary>
        /// Создает динамик-метод установки значения поля по типам объекта и значения.
        /// </summary>
        private DynamicMethod CreateSetter(FieldInfo field, params Type[] paramTypes) {
            var dm = new DynamicMethod("setter_" + field.Name, typeof(void), paramTypes, field.DeclaringType);
            var gen = dm.GetILGenerator();
            if (paramTypes[0].IsValueType)
                gen.Emit(OpCodes.Ldarga, 0);
            else
                gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Stfld, field);
            gen.Emit(OpCodes.Ret);
            return dm;
        }

        /// <summary>
        /// Создает динамик-метод установки значения свойства по типам объекта и значения.
        /// </summary>
        private DynamicMethod CreateSetter(PropertyInfo property, params Type[] paramTypes) {
            var dm = new DynamicMethod("setter_" + property.Name, typeof(void), paramTypes, property.DeclaringType);
            var gen = dm.GetILGenerator();
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            var method = property.GetSetMethod(true);
            gen.EmitCall(method.IsVirtual ? OpCodes.Callvirt : OpCodes.Call, method, null);
            gen.Emit(OpCodes.Ret);
            return dm;
        }

        /// <summary>
        /// Создает делегат конструирования объекта без параметров.
        /// </summary>
        public ConstructorDelegate<T> CreateConstructor<T>() where T : class {
            var objectType = typeof(T);
            var ci = objectType.GetConstructor(Type.EmptyTypes);
            if (ci == null)
                throw new SerializationException(string.Format("Type '{0}' doesn't have a constructor without parameters.", objectType));
            var dm = new DynamicMethod("constructor", objectType, Type.EmptyTypes);
            var gen = dm.GetILGenerator();
            gen.Emit(OpCodes.Newobj, ci);
            gen.Emit(OpCodes.Ret);
            return CreateDelegate<ConstructorDelegate<T>>(dm);
        }

        /// <summary>
        /// Создает делегат конструирования объекта с параметрами указанных типов.
        /// </summary>
        public ParametrizedConstructorDelegate<T> CreateConstructor<T>(Type[] parameterTypes) {
            var objectType = typeof(T);
            var ctorInfo = objectType.GetConstructor(parameterTypes);
            if (ctorInfo == null) {
                var messageBuilder = new StringBuilder().Append(parameterTypes, type => type.Name, ", ");
                throw new SerializationException(string.Format("Type '{0}' doesn't have any constructors with requested signature: ({1})", objectType, messageBuilder));
            }

            var dm = new DynamicMethod("constructor", objectType, new[] { typeof(object[]) });            var gen = dm.GetILGenerator();

            for (int i = 0; i < parameterTypes.Length; i++) {
                var parameterType = parameterTypes[i];
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Ldc_I4, i);
                gen.Emit(OpCodes.Ldelem_Ref);

                if (parameterType.IsValueType)
                    gen.Emit(OpCodes.Unbox_Any, parameterType);
                else {
                    gen.Emit(OpCodes.Isinst, parameterType);
                }
            }

            gen.Emit(OpCodes.Newobj, ctorInfo);
            gen.Emit(OpCodes.Ret);

            return CreateDelegate<ParametrizedConstructorDelegate<T>>(dm);
        }

        private static T CreateDelegate<T>(DynamicMethod dm) where T : class {
            return dm.CreateDelegate(typeof(T)) as T;
        }
    }
}

