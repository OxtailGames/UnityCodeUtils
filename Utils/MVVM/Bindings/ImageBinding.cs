using UnityEngine;
using UnityEngine.UI;

namespace Oxtail.Utils.MVVM
{
    [RequireComponent(typeof(Image))]
    [DataBindingType(typeof(Sprite))]
    public class ImageBinding : DataBinding
    {
        private Image m_Image;

        protected override void Awake()
        {
            base.Awake();
            m_Image = GetComponent<Image>();
        }

        public override void SetData(object data)
        {
            if (data is not Sprite sprite)
                return;

            if (m_Image == null)
            {
                if (!TryGetComponent(out m_Image))
                    return;
            }

            m_Image.sprite = sprite;
        }
    }
}
