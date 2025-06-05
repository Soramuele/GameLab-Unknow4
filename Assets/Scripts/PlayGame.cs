using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public SceneReference essentialScene;
    public SceneReference NextScene;

    public void PlayDaGame()
    {
        
        SceneManager.LoadScene(NextScene, LoadSceneMode.Single);

        Debug.Log("I love Onions!");
    }
}
