using UnityEngine;
using UnityEngine.AI;

public class NPCSpawner : MonoBehaviour
{
    [Header("NPC")]
    [SerializeField] private GameObject npcPrefab;
    [SerializeField, Range(0.5f, 10f)] private float spawnRate = 3;
    
    private float timePassed;

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= spawnRate)
        {
            timePassed = 0;
            Debug.Log("Spawn");

            SpawnNPC();
        }
    }

    private void SpawnNPC()
    {
        float spawnPosX = Random.Range(-24, 0);
        float spawnPosZ = Random.Range(-30, -33);

        var spawnPosition = new Vector3(spawnPosX, 2, spawnPosZ);

        var clone = Instantiate(npcPrefab);
        clone.transform.position = spawnPosition;
        
    }
}
