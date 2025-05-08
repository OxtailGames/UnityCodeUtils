using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Oxtail.Utils
{
    [CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
    public class ConditionalHideDrawer : PropertyDrawer
    {     
        private ConditionalHideAttribute m_ConditionalAttribute => (ConditionalHideAttribute)attribute;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (AttributeIsEnabled(property) == m_ConditionalAttribute.BooleanCondition)
                EditorGUI.PropertyField(position, property, label, true);

        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            bool enabled = AttributeIsEnabled(property) == m_ConditionalAttribute.BooleanCondition;

            if (enabled)
                return EditorGUI.GetPropertyHeight(property, label);
            else
                return -EditorGUIUtility.standardVerticalSpacing;
        }

        private bool AttributeIsEnabled(SerializedProperty property)
        {
            string conditionalPropertyPath = property.propertyPath.Replace(property.name, m_ConditionalAttribute.ConditionalField);
            SerializedProperty serializedConditionalProperty = property.serializedObject.FindProperty(conditionalPropertyPath);

            if (serializedConditionalProperty != null && serializedConditionalProperty.propertyType == SerializedPropertyType.Boolean)
                return serializedConditionalProperty.boolValue;

            return false;
        }
    }
}
