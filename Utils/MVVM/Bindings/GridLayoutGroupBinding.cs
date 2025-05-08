using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Oxtail.Utils.MVVM
{
    [RequireComponent(typeof(GridLayoutGroup))]
    [DataBindingType(typeof(List<GameObject>))]
    public class GridLayoutGroupBinding : DataBinding
    {
        private GridLayoutGroup m_Grid;

        protected override void Awake()
        {
            base.Awake();
            m_Grid = GetComponent<GridLayoutGroup>();
        }

        public override void SetData(object data)
        {
            if (data is not List<GameObject> gameObjects)
                return;

            if (m_Grid == null)
            {
                if (!TryGetComponent(out m_Grid))
                    return;
            }

            foreach (GameObject go in gameObjects)
            {
                go.transform.parent = m_Grid.transform;
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(m_Grid.RectTransform());
        }
    }
}
