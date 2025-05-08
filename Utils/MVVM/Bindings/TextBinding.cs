using TMPro;
using UnityEngine;

namespace Oxtail.Utils.MVVM
{
    [RequireComponent(typeof(TMP_Text))]
    [DataBindingType(typeof(string))]
    public class TextBinding : DataBinding
    {
        private TMP_Text m_Text;

        protected override void Awake()
        {
            base.Awake();
            m_Text = GetComponent<TMP_Text>();
        }

        public override void SetData(object data)
        {
            if (data is not string text)
                return;

            if (m_Text is null)
            {
                if (!TryGetComponent(out m_Text))
                    return;
            }

            m_Text.text = text;
        }
    }
}
