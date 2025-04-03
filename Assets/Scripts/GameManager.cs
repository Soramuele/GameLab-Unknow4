using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private List<string> sceneNames = new();

    public float InGameTimer { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            sceneNames.Add(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)));
    }

    // Update is called once per frame
    void Update()
    {
        InGameTimer += Time.deltaTime;
    }

    public void MoveToNextScene(string level)
    {
        if (!sceneNames.Contains(level))
        {
            Debug.Log($"Ops, wrong name. Check carefully how you wrote: {level}");
            return;
        }

        if (level == "TitleScreen")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        SceneManager.LoadSceneAsync(level);
    }

    public void QuitApplication()
    {
        // This will close the game in a build
        Application.Quit();

        // If in the editor, this will stop the play mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
