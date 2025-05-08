using UnityEngine;
using UnityEngine.UI;

namespace Oxtail.Utils.MVVM
{
    [RequireComponent(typeof(Button))]
    [DataBindingType(typeof(bool))]
    public class InteractableButtonBinding : DataBinding
    {
        private Button m_Button;

        protected override void Awake()
        {
            base.Awake();
            m_Button = GetComponent<Button>();
        }

        public override void SetData(object data)
        {
            if (data is not bool interactable)
                return;

            if (m_Button == null)
            {
                if (!TryGetComponent(out m_Button))
                    return;
            }

            m_Button.interactable = interactable;
        }
    }
}

