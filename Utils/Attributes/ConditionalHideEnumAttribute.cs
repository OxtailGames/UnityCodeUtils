using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Oxtail.Utils
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true)]
    public class ConditionalHideEnumAttribute : PropertyAttribute
    {
        public string ConditionalField { get; private set; }
        public string EnumValue { get; private set; }

        public ConditionalHideEnumAttribute(string conditionalField, string enumValue)
        {
            ConditionalField = conditionalField;
            EnumValue = enumValue;
        }
    }
}