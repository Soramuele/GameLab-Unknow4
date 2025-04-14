using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            EndLevel();
    }

    private void EndLevel()
    {
        Debug.Log("You finished this level. Congrats!");
        Debug.Break();
    }
}
