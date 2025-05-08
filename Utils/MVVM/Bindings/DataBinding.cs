using UnityEngine;

namespace Oxtail.Utils.MVVM
{
    [DefaultExecutionOrder(-1)]
    public abstract class DataBinding : MonoBehaviour
    {
        [SerializeField] private string m_FieldName;

        public string FieldName => m_FieldName;

        protected virtual void Awake()
        {
            GetComponentInParent<ViewModel>().AddViewBinding(this);
        }

        public abstract void SetData(object data);
    }
}
