using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private bool canPass = false;

    void Start()
    {
        if (sceneName != "")
            StartCoroutine(WaitThenDeactivateWall());
    }

    void OnTriggerEnter(Collider collider)
    {
        // if (collider.tag == "Player")
        //     if (sceneName != "" && canPass)
        //         GameManager.Instance.MoveToNextScene(sceneName);
    }

    private System.Collections.IEnumerator WaitThenDeactivateWall()
    {
        yield return new WaitForSeconds(20);
        GetComponent<MeshRenderer>().enabled = false;
        canPass = true;
        GetComponent<BoxCollider>().isTrigger = true;
    }
}
