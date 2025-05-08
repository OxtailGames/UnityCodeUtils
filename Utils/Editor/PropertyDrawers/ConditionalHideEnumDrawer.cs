using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Oxtail.Utils
{
    [CustomPropertyDrawer(typeof(ConditionalHideEnumAttribute))]
    public class ConditionalHideEnumDrawer : PropertyDrawer
    {
        private ConditionalHideEnumAttribute m_ConditionalAttribute => (ConditionalHideEnumAttribute)attribute;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (AttributeIsValid(property))
                EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (AttributeIsValid(property))
                return EditorGUI.GetPropertyHeight(property, label);
            else
                return -EditorGUIUtility.standardVerticalSpacing;
        }

        private bool AttributeIsValid(SerializedProperty property)
        {
            string conditionalPropertyPath = property.propertyPath.Replace(property.name, m_ConditionalAttribute.ConditionalField);
            SerializedProperty serializedConditionalProperty = property.serializedObject.FindProperty(conditionalPropertyPath);

            if (serializedConditionalProperty != null && serializedConditionalProperty.propertyType == SerializedPropertyType.Enum)
            {
                if (m_ConditionalAttribute.EnumValue == serializedConditionalProperty.enumNames[serializedConditionalProperty.enumValueIndex])
                    return true;
            }

            return false;
        }
    }
}
