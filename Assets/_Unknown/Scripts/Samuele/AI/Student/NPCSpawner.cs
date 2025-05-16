using System.Collections.Generic;
using UnityEngine;

namespace Unknown.Samuele
{
    public class NPCSpawner : MonoBehaviour
    {
        [Header("NPC")]
        [SerializeField] private GameObject npcPrefab;
        [SerializeField, Range(0.5f, 10f)] private float spawnRate = 3;
        [SerializeField] private int npcMax = 5;
        
        private static Dictionary<GameObject, bool> npcs = new Dictionary<GameObject, bool>();

        private float timePassed;

        // Update is called once per frame
        void Update()
        {
            timePassed += Time.deltaTime;

            if (timePassed >= spawnRate)
            {
                timePassed = 0;

                if (npcs.Count < npcMax)
                    SpawnNPC();
            }
        }

        private void SpawnNPC()
        {
            var clone = Instantiate(npcPrefab, GetRandomPosition(), Quaternion.identity, transform);

            SubscribeToUsedNPC(clone);

            // var spawnPosition = GetRandomPosition();
            // clone.transform.position = spawnPosition;
        }

        private static Vector3 GetRandomPosition()
        {
            float spawnPosX = Random.Range(-24, 0);
            float spawnPosZ = Random.Range(-30, -33);

            var position = new Vector3(spawnPosX, 1, spawnPosZ);
            return position;
        }

        public void SubscribeToUsedNPC(GameObject npc)
        {
            npcs.Add(npc, true);
        }

        public static void NPCIsUnused(GameObject npc)
        {
            if (npcs.ContainsKey(npc))
            {
                npcs[npc] = true;
                npc.SetActive(true);
                npc.transform.position = GetRandomPosition();
                // npc.GetComponent<StudentMovement>().isAtDoor = false;
                // npc.GetComponent<StudentMovement>().Start();
            }
            else
            {
                throw new System.NotImplementedException("Something went wrong with your npc");
            }
        }
    }
}
