using System;

namespace Crocodev.Common.SmartConfiguration
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class SectionNameAttribute : Attribute
    {
        public SectionNameAttribute(string sectionName)
        {
            Name = sectionName;
        }

        public string Name { get; private set; }
    }
}