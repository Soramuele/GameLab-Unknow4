using UnityEngine;

namespace Unknown.Samuele
{
    public class MinigameHandler : MonoBehaviour
    {
        [Header("Inputs")]
        [SerializeField] private InputHandler inputs;

        [Header("Screens")]
        [SerializeField] private GameObject mainScreen;
        [SerializeField] private GameObject endScreen;
        [SerializeField] private MinigameLevel[] minigameLevels;

        [HideInInspector] public int LevelReached = 0;

        void OnEnable() =>
            inputs.BackEvent += CloseMinigame;

        void OnDisable() =>
            inputs.BackEvent -= CloseMinigame;

        // Update is called once per frame
        void Update()
        {
            // Check if level is ready to be played
            if (!minigameLevels[LevelReached].CanPlay)
                return;
            
            // Play the minigame
        }

        public void SetupMinigame()
        {
            mainScreen.SetActive(true);
            endScreen.SetActive(false);
            foreach(var level in minigameLevels)
                level.gameObject.SetActive(false);
        }

        public void StartMinigame()
        {
            mainScreen.SetActive(false);
            minigameLevels[LevelReached].gameObject.SetActive(true);
            minigameLevels[LevelReached].StartGame(this);
        }

        private void CloseMinigame()
        {
            // Close the minigame
            gameObject.SetActive(false);

            // Switch back to gameplay inputs
            inputs.SetGameplay();
        }
        // TODO: Show appropriate level
        // TODO: Play the selected level
    }
}
