using UnityEngine;

namespace Oxtail.Utils.MVVM
{
    [DataBindingType(typeof(Vector3))]
    public class TransformScaleBinding : DataBinding
    {
        public override void SetData(object data)
        {
            if (data is not Vector3 scale)
                return;

            transform.localScale = scale;
        }
    }
}

