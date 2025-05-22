using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    [Header("Scene to load")]
    [SerializeField] private SceneReference scene;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            EndLevel();
    }

    private void EndLevel()
    {
        Debug.Log("You finished this level. Congrats!");
        SceneManager.LoadScene(scene);
    }
}
