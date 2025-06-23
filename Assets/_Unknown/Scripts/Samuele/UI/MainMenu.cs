using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unknown.Samuele
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Scenes")]
        [SerializeField] private SceneReference gameScene;
        [SerializeField] private SceneReference persistantScene;

        public void PlayGame()
        {
            // Change scene to game scene
            SceneManager.LoadScene(gameScene);
            SceneManager.LoadSceneAsync(persistantScene, LoadSceneMode.Additive);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}
