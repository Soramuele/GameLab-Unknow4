using System.Collections.Generic;
using UnityEngine;

namespace Unknown.Samuele
{
    public class PipeSpawner : Pausable
    {
        [SerializeField] private GameObject pipesPrefab;
        [SerializeField] private float spawnRate = 1f;
        [SerializeField] private float minHeight = -1f;
        [SerializeField] private float maxHeight = 2f;

        private bool canSpawn = false;
        private List<GameObject> pipes = new();

        private void OnEnable()
        {
            InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(Spawn));
        }

        private void Spawn()
        {
            if (!canSpawn)
                return;

            var pipe = Instantiate(pipesPrefab, transform.position, Quaternion.identity, transform);
            pipe.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
            
            pipes.Add(pipe);
        }

        public void RemovePipe(GameObject pipe)
        {
            pipes.Remove(pipe);
        }

        public void Pause()
        {
            canSpawn = false;

            foreach (var _pipe in pipes)
            {
                _pipe.GetComponent<Pipes>().CanMove = false;
            }
        }

        public void StartGame()
        {
            canSpawn = true;

            foreach (var _pipe in pipes)
                Destroy(_pipe);

            pipes.Clear();
        }
    }
}
