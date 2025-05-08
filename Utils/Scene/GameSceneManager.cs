using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Oxtail.Utils
{
    public static class GameSceneManager
    {
        public enum SceneLoadMode
        {
            Sync,
            Async
        }

        public static void LoadScene(string sceneName, SceneLoadMode mode)
        {
            switch (mode)
            {
                case SceneLoadMode.Sync:
                    LoadSceneSync(sceneName);
                    break;
                case SceneLoadMode.Async:
                    LoadSceneAsync(sceneName);
                    break;
            }
        }

        private static void LoadSceneSync(string scene)
        {
            Scene previousScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);

            if (previousScene.IsValid())
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(previousScene);
        }

        private static async Task LoadSceneAsync(string scene)
        {
            Scene previousScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

            AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
            op.allowSceneActivation = true;

            await op;

            if (previousScene.IsValid())
                await UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(previousScene);
        }
    }
}
