using System.Collections;
using UnityEngine;

namespace Unknown.Samuele
{
    public class MazeLevel : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private MazePlayer player;

        [Header("Blocks")]
        [SerializeField] private GameObject startBlock;

        [Header("VFX")]
        [SerializeField] private ParticleSystem spawnParticle;
        [SerializeField] private ParticleSystem dieParticle;

        void OnEnable()
        {
            player.OnDieEvent += OnPlayerDie;
        }

        void OnDisable()
        {
            player.OnDieEvent -= OnPlayerDie;
        }

        private void OnPlayerDie()
        {
            Instantiate(dieParticle, player.transform.position, Quaternion.identity, transform);

            player.gameObject.SetActive(false);

            StartCoroutine(RespawnPlayer());
        }

        private IEnumerator RespawnPlayer()
        {
            PositionPlayer();

            yield return new WaitForSeconds(dieParticle.main.duration);

            SpawnPlayer();
        }

        private void PositionPlayer()
        {
            player.transform.position = startBlock.transform.position;
        }

        private void SpawnPlayer()
        {
            player.gameObject.SetActive(true);
            Instantiate(spawnParticle, player.transform.position, Quaternion.identity, transform);
        }
    }
}
