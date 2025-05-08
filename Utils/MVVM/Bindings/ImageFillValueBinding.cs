using UnityEngine;
using UnityEngine.UI;

namespace Oxtail.Utils.MVVM
{
    [RequireComponent(typeof(Image))]
    [DataBindingType(typeof(float))]
    public class ImageFillValueBinding : DataBinding
    {
        private Image m_Image;

        protected override void Awake()
        {
            base.Awake();
            m_Image = GetComponent<Image>();
        }

        public override void SetData(object data)
        {
            if (data is not float value)
                return;

            if (m_Image == null)
            {
                if (!TryGetComponent<Image>(out m_Image))
                    return;
            }

            m_Image.fillAmount = value;
        }
    }
}
