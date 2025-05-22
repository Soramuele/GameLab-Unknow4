using System.Collections;
using UnityEngine;

namespace Unknown.Samuele
{
    public class MGLevel : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private MinigamePlayerControls player;
        
        [Header("Level Objects")]
        [SerializeField] private GameObject startBlock;

        [Header("VFX")]
        [SerializeField] private ParticleSystem spawnEffect;
        [SerializeField] private ParticleSystem dieEffect;

        void OnEnable() =>
            player.OnDieEvent += OnPlayerDie;

        void OnDisable() =>
            player.OnDieEvent -= OnPlayerDie;

        public void StartGame() =>
            StartCoroutine(SpawnPlayer());

        private void OnPlayerDie()
        {
            // Disable player inputs
            player.CanMove = false;

            // Spawn effects for the player death
            Instantiate(dieEffect, player.transform.position, Quaternion.identity);
            // PlayAudio of player death

            // Disable player
            player.gameObject.SetActive(false);

            // Spawn player with vfx
            StartCoroutine(SpawnPlayer());
        }

        private IEnumerator SpawnPlayer()
        {
            // Move player to the start
            player.transform.position = startBlock.transform.position;

            // Wait a bit for death particles
            yield return new WaitForSeconds(dieEffect.main.duration * .5f);

            // Play spawn effects
            var _particles = Instantiate(spawnEffect, player.transform.position, Quaternion.identity);
            // PlayAudio of player spawning

            // Wait a bit for spawn particles
            yield return new WaitForSeconds(_particles.main.duration * .9f);

            // Enable player + player inputs
            player.gameObject.SetActive(true);
            player.CanMove = true;

        }
    }
}