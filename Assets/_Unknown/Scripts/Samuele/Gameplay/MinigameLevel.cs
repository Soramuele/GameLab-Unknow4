using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Unknown.Samuele
{
    public class MinigameLevel : MonoBehaviour
    {
        [Header("Level objects")]
        [SerializeField] private RectTransform player;
        [SerializeField] private RectTransform startPosition;
        [SerializeField] private RectTransform endPosition;
        [SerializeField] private Image wall;

        [Header("VFX")]
        [SerializeField] private GameObject spawnEffect;
        [SerializeField] private GameObject dieEffect;

        private MinigameHandler handler;
        private bool canPlay = false;
        public bool CanPlay => canPlay;

        void Update()
        {
            if (!gameObject.activeSelf && canPlay)
                canPlay = false;
        }

        public void StartGame(MinigameHandler _handler)
        {
            handler = _handler;
            // Put player at the start of the level
            player.position = startPosition.position;
            // Animate player spawn and after finish animation set canPlay = true;
            StartCoroutine(SpawnPlayer());
        }

        private IEnumerator SpawnPlayer()
        {
            player.gameObject.SetActive(true);
            yield return null;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider == wall.GetComponent<BoxCollider2D>())
            {
                // Animate player death, then call this
                StartGame(handler);
            }
            else if (collision.collider == endPosition.GetComponent<BoxCollider2D>())
            {
                // You reached the end of the level
                // Switch to a new level
                handler.LevelReached++;
                handler.StartMinigame();
            }
        }
    }
}
