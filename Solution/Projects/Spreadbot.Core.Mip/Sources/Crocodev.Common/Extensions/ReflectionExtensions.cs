using System;
using System.Reflection;

namespace Crocodev.Common
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Obtain attribute settled type. Can obtain inheretted attributes. Returns nillif attribute is not found.
        /// </summary>
        public static T GetAttribute<T>(
            this MemberInfo member,
            bool inherit = false) where T : Attribute
        {
            var attr = Attribute.GetCustomAttribute(member, typeof (T), inherit);
            return attr as T;
        }
    }
}