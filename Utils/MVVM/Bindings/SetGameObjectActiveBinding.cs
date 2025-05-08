using System.Net;
using UnityEngine;

namespace Oxtail.Utils.MVVM
{
    [DataBindingType(typeof(bool))]
    public class SetGameObjectActiveBinding : DataBinding
    {
        public override void SetData(object data)
        {
            if (data is not bool active)
                return;

            gameObject.SetActive(active);
        }
    }
}
