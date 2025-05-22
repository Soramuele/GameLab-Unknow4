using System.Collections.Generic;
using UnityEngine;

namespace Unknown.Samuele
{
    public class PipeSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float spawnRate = 1f;
        [SerializeField] private float minHeight = -1f;
        [SerializeField] private float maxHeight = 2f;

        private List<GameObject> myPipes;
        private bool isPaused = false;

        public void Pause()
        {
            isPaused = true;

            foreach (var pipe in myPipes)
                pipe.GetComponent<Pipes>().Pause();
        }
        
        public void Resume()
        {
            isPaused = false;

            foreach (var pipe in myPipes)
                pipe.GetComponent<Pipes>().Resume();
        }

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
            if (isPaused)
                return;

            GameObject pipe = Instantiate(prefab, transform.position, Quaternion.identity, transform);
            pipe.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
            pipe.GetComponent<Pipes>().Spawner(this);

            myPipes.Add(pipe);
        }

        public void RemovePipe(Pipes pipe) =>
            myPipes.Remove(pipe.gameObject);
    }
}
