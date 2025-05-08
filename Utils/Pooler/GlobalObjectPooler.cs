using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oxtail.Utils
{
    public class GlobalObjectPooler : MonoSingleton<GlobalObjectPooler>
    {
        [System.Serializable]
        private struct Pool
        {
            public GameObject Obj;
            public int Count;
            public string ID;
        }

        [SerializeField] private List<Pool> m_Pools = new List<Pool>();

        private Dictionary<string, List<GameObject>> m_CreatedPools = new Dictionary<string, List<GameObject>>();

        protected override void Awake()
        {
            base.Awake();

            foreach(Pool pool in m_Pools)
            {
                if (pool.Count <= 0)
                    continue;

                List<GameObject> gameObjects = new List<GameObject>();
                GameObject parent = new GameObject(pool.ID);
                parent.transform.parent = transform;

                ExpandList(pool.ID, pool.Obj, pool.Count, ref gameObjects);

                m_CreatedPools.Add(pool.ID, gameObjects);
            }
        }


        /// <summary>
        /// Get the first not active GameObject in the list. If all are active returns the first object.
        /// If expandList is true and no active objects are available, expands the list with new objects and returns the new created object.  
        /// </summary>
        public GameObject GetObject(string id, bool expandList)
        {
            List<GameObject> list = m_CreatedPools[id];
            GameObject retGameObject = null;

            foreach (GameObject obj in list)
            {
                if (!obj.activeInHierarchy)
                {
                    retGameObject = obj;
                    break;
                }
            }

            if (retGameObject == null)
            {
                if (expandList)
                {
                    ExpandList(id, list[0], 1, ref list);
                    retGameObject = list[list.Count - 1];
                }
                else
                {
                    retGameObject = list[0];
                    list.RemoveAt(0);
                    list.Add(retGameObject);
                }
            }

            return retGameObject;
        }

        private void ExpandList(string id, GameObject obj, int count, ref List<GameObject> gameObjects) 
        {
            Transform parent = transform.Find(id);
            for (int i = 0; i < count; i++)
            {
                GameObject instantiated = Instantiate(obj, parent);
                instantiated.SetActive(false);
                gameObjects.Add(instantiated);
            }
        }
    }
}
