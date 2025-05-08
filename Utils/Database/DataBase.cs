using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oxtail.Utils
{
    [System.Serializable]
    public struct DatabaseEntry<Object>
    {
        public string Key;
        public Object Value;
    }

    public class DataBase<Database, Object> : ScriptableObjectSingleton<Database> where Database : ScriptableObject
    {
        [SerializeField] protected List<DatabaseEntry<Object>> m_Entries = new List<DatabaseEntry<Object>>();
        
        public List<DatabaseEntry<Object>> Entries => m_Entries;

        public void AddEntry(string key, Object value, int index = -1)
        {
            foreach (DatabaseEntry<Object> entry in m_Entries)
            {
                if (entry.Key == key)
                    return;
            }

            DatabaseEntry<Object> newEntry = new DatabaseEntry<Object> { Key = key, Value = value };
            if (index == -1)
                m_Entries.Add(newEntry);
            else
                m_Entries.Insert(index, newEntry);
        }

        public void RemoveEntry(string key)
        {
            foreach (DatabaseEntry<Object> entry in m_Entries)
            {
                if (entry.Key == key)
                {
                    m_Entries.Remove(entry);
                    return;
                }
            }
        }

        public void ChangeValue(string key, Object value)
        {
            int index = -1;

            foreach (DatabaseEntry<Object> entry in m_Entries)
            {
                if (entry.Key == key)
                {
                    index = m_Entries.IndexOf(entry);
                    break;
                }
            }
            
            RemoveEntry(key);
            AddEntry(key, value, index);
        }

        public bool TryGetValue(string key, out Object value)
        {
            value = default;

            foreach (DatabaseEntry<Object> entry in m_Entries)
            {
                if (entry.Key == key)
                {
                    value = entry.Value;
                    return true;
                }
            }

            return false;
        }

        public Object GetRandomValue()
        {
            return m_Entries[Random.Range(0, m_Entries.Count)].Value;
        }
    }
}
