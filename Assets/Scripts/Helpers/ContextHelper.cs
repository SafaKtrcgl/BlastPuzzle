using UnityEngine.SceneManagement;

namespace Helpers
{
    public class ContextHelper : HelperBase
    {
        private const string InitializationScene = "InitializationScene";
        private const string MainScene = "MainScene";
        private const string GameplayScene = "GameplayScene";

        public void LoadInitializationScene() => LoadScene(InitializationScene);
        public void LoadMainScene() => LoadScene(MainScene);
        public void LoadGameplayScene() => LoadScene(GameplayScene);

        private void LoadScene(string sceneName)
        {
            HelperResources.Instance.RemoveHelpers();
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
