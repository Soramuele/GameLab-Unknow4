using UnityEngine;

public class NPCFacing : MonoBehaviour
{
    private Transform playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        var playerController = GameObject.FindGameObjectWithTag("Player");
        playerPosition = playerController.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = playerPosition.position - transform.position;
        direction.y = 0f; // Lock vertical rotation so NPC only rotates on Y axis

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }
}
