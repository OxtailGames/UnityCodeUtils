using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;

namespace Oxtail.Utils.MVVM
{
    [CustomEditor(typeof(DataBinding), true)]
    public class DataBindingEditor : Editor
    {
        //Field
        private SerializedProperty m_FieldNameProperty;
        private Type m_BindingClass;
        private Type m_FieldType;
        private int m_FieldIndex;
        private string[] m_FieldsNames;

        protected void OnEnable()
        {
            m_FieldNameProperty = serializedObject.FindProperty("m_FieldName");
            m_FieldType = target.GetType().GetCustomAttribute<DataBindingTypeAttribute>().PropertyType;

            GetViewModelBindingFields();
        }

        private void GetViewModelBindingFields()
        {
            m_BindingClass = (target as DataBinding).GetComponentInParent<ViewModel>().GetType();
            m_FieldsNames = GetAllFieldsName().ToArray();

            for (int i = 0; i < m_FieldsNames.Length; i++)
            {
                if (m_FieldNameProperty.stringValue != m_FieldsNames[i])
                    continue;

                m_FieldIndex = i;
                break;
            }
        }

        private List<string> GetAllFieldsName()
        {
            List<string> fields = new List<string>();
            Type currentClass = m_BindingClass;

            while (currentClass != typeof(ViewModel))
            {
                IEnumerable<FieldInfo> properties = currentClass.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    .Where(x => x.FieldType == m_FieldType && Attribute.GetCustomAttribute(x, typeof(DataBindingFieldAttribute)) != null);
            
                foreach (FieldInfo field in properties)
                {
                    fields.Add(field.Name);
                }

                currentClass = currentClass.BaseType;
            }

            return fields;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (m_FieldsNames.Length > 0)
            {
                m_FieldIndex = EditorGUILayout.Popup("Binding property", m_FieldIndex, m_FieldsNames);
                m_FieldNameProperty.stringValue = m_FieldsNames[m_FieldIndex];
            }
            else if (m_BindingClass is null)
                EditorGUILayout.TextArea($"View Model {m_BindingClass.Name} not found in parent GameObject");
            else
                EditorGUILayout.TextArea($"Binding of type {m_FieldType.Name} not found in {m_BindingClass.Name}");

            serializedObject.ApplyModifiedProperties();
        }
    }
}
