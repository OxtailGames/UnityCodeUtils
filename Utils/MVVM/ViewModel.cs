using System.Collections.Generic;
using UnityEngine;

namespace Oxtail.Utils.MVVM
{
    public abstract class ViewModel : MonoBehaviour
    {
        protected List<DataBinding> m_Views = new List<DataBinding>();

        public string GetID() => GetType().Name;

        public void AddViewBinding(DataBinding binding)
        {
            m_Views.Add(binding);
        }

        public void UpdateBindingData(string varName, object data)
        {
            foreach (DataBinding view in m_Views)
            {
                if (view.FieldName != varName)
                    continue;

                view.SetData(data);
                break;
            }
        }
    }
}
