/*
 *  $Id: AttrTypeResolver.cs 181 2010-11-27 11:36:49Z thenn $
 *  This file is a part of Nereal Serialization library.
 *  (C) 2010 Tkhenn Erannor/Nereal Software
 *
 */

using System;
using System.Reflection;

using Nereal.Extensions;

namespace Nereal.Serialization {
    /// <summary>
    /// Сканирующий определитель типов: автоматически регистрирует типы по определенным базовому типу и атрибуту.
    /// </summary>
    public class AttrTypeResolver<T, A> : DictTypeResolver where A : Attribute, IKeyed<string> {
        /// <summary>
        /// Создает новый экземпляр сканирующего определителя типов.
        /// </summary>
        public AttrTypeResolver(string attrName) : base(attrName) {
        }

        /// <summary>
        /// Регистрирует все уже загруженные сборки и подписывается на событие загрузки новых сборок.
        /// </summary>
        protected override void RegisterTypes() {
            Array.ForEach(AppDomain.CurrentDomain.GetAssemblies(), RegisterAssembly);
            AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoad;
        }

        /// <summary>
        /// Регистрирует все подходящие типы из указанной сборки.
        /// </summary>
        private void RegisterAssembly(Assembly asm) {
            if (SerializationConfig.IsIgnoreScanAssembly(asm))
                return;
            foreach (var v in asm.GetChildTypes<A>(typeof(T)))
                Register(v.Item1.Key, v.Item2);
        }

        private void OnAssemblyLoad(object sender, AssemblyLoadEventArgs args) {
            RegisterAssembly(args.LoadedAssembly);
        }
    }

    /// <summary>
    /// Сканирующий определитель типов: автоматически регистрирует типы по определенному базовому типу и атрибуту TypeIdAttribute.
    /// </summary>
    public class AttrTypeResolver<T> : AttrTypeResolver<T, TypeIdAttribute> {
        /// <summary>
        /// Создает новый экземпляр сканирующего определителя типов, использующего TypeIdAttribute.
        /// </summary>
        public AttrTypeResolver(string attrName) : base(attrName) {
        }
    }
}
