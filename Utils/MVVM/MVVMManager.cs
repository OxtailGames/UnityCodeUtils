using System;
using System.Collections.Generic;
using UnityEngine;

namespace Oxtail.Utils.MVVM
{
    public class MVVMManager : MonoSingleton<MVVMManager>
    {
        private List<ViewModel> m_ModelViews = new List<ViewModel>();

        public void AddModelView(ViewModel modelView)
        {
            if (!m_ModelViews.Contains(modelView))
                m_ModelViews.Add(modelView);
            else
                Debug.LogError($"Model View {nameof(modelView)} is already added!");
        }

        public void UpdateData(string mvID, string varName, object data)
        {
            foreach (ViewModel modelView in m_ModelViews)
            {
                if (modelView.GetID() != mvID)
                    continue;

                modelView.UpdateBindingData(varName, data);
                break;
            }
        }
    }
}
