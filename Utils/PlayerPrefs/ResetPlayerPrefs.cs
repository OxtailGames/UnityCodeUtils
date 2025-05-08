using UnityEditor;
using UnityEngine;

namespace Oxtail.Utils
{
#if UNITY_EDITOR
    public class ResetPlayerPrefs : EditorWindow
    {
        [MenuItem("Edit/Reset Playerprefs")] 
        public static void DeletePlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
#endif
}

