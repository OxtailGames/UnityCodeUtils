using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace Oxtail.Utils
{
    [Serializable]
    public class SerializedDictionary<Key, Value> : Dictionary<Key, Value>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<Key> m_Keys = new List<Key>(); 
        [SerializeField] private List<Value> m_Values = new List<Value>();

        public SerializedDictionary() : base()
        {
            m_Keys = new List<Key>();
            m_Values = new List<Value>();
        }

        public SerializedDictionary(Dictionary<Key, Value> dictionary) : base(dictionary)
        {
            m_Keys = new List<Key>();
            m_Values = new List<Value>();
        }

        public SerializedDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public void OnAfterDeserialize()
        {
            for(int i = 0; i < m_Keys.Count; i++)
                Add(m_Keys[i], m_Values[i]);

            m_Keys.Clear();
            m_Values.Clear();
        }

        public void OnBeforeSerialize()
        {
            m_Keys.Clear();
            m_Values.Clear();

            foreach (var entry in this)
            {
                m_Keys.Add(entry.Key);
                m_Values.Add(entry.Value);
            }
        }
    }
}
