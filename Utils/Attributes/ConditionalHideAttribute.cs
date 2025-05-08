using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Oxtail.Utils
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true)]
    public class ConditionalHideAttribute : PropertyAttribute
    {
        public string ConditionalField { get; private set; }
        public bool BooleanCondition { get; private set; }

        public ConditionalHideAttribute(string conditionalField, bool stateCondition)
        {
            ConditionalField = conditionalField;
            BooleanCondition = stateCondition;
        }
    }
}
