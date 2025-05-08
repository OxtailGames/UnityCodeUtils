using System;
using UnityEngine;

namespace Oxtail.Utils.MVVM
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class DataBindingTypeAttribute : Attribute
    {
        public Type PropertyType { get; private set; }

        public DataBindingTypeAttribute(Type propertyType) => PropertyType = propertyType;
    }
}
