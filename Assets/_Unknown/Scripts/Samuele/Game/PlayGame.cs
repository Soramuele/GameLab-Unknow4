using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public SceneReference essentialScene;
    public SceneReference NextScene;

    public void PlayDaGame()
    {
        SceneManager.LoadScene(essentialScene, LoadSceneMode.Single);
        SceneManager.LoadScene(NextScene, LoadSceneMode.Additive);

        Debug.Log("I love Onions!");
    }
}
