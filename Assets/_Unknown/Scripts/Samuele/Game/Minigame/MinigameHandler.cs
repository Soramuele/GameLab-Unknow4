using Cinemachine;
using UnityEngine;

namespace Unknown.Samuele
{
    public class MinigameHandler : MonoBehaviour
    {
        [Header("Inputs")]
        [SerializeField] private InputHandler inputs;

        [Header("PLayer")]
        [SerializeField] private MinigamePlayerControls player;
        [SerializeField] private CinemachineVirtualCamera cam;

        [Header("Screens")]
        [SerializeField] private GameObject mainScreen;
        [SerializeField] private GameObject endScreen;
        [SerializeField] private MGLevel[] minigameLevels = new MGLevel[3];

        private GameObject currentLevel;
        private int levelReached = 0;

        void Start()
        {
            mainScreen.SetActive(true);
            endScreen.SetActive(false);
            foreach(var level in minigameLevels)
                level.gameObject.SetActive(false);
        }

        void OnEnable()
        {
            // CameraManager.Instance.BlendingCompleteEvent += () => CameraManager.Instance.SwitchCamera(cam);
            inputs.BackEvent += CloseMinigame;
            player.FinishReachedEvent += NextLevel;
        }
        
        void OnDisable()
        {
            // CameraManager.Instance.BlendingCompleteEvent -= () => CameraManager.Instance.SwitchCamera(cam);
            inputs.BackEvent -= CloseMinigame;
            player.FinishReachedEvent -= NextLevel;
        }

        public void StartMinigame()
        {
            mainScreen.SetActive(false);
            
            SetupLevel();
        }

        private void NextLevel()
        {
            if (levelReached < minigameLevels.Length - 1)
            {
                currentLevel.SetActive(false);
                levelReached++;
                
                SetupLevel();
            }
            else
            {
                // Stops player
                player.CanMove = false;
                player.gameObject.SetActive(false);

                // Clear levels
                currentLevel.SetActive(false);
                levelReached = 0;
                currentLevel = null;

                // Show end screen
                endScreen.SetActive(true);
            }
        }

        private void SetupLevel()
        {
            currentLevel = minigameLevels[levelReached].gameObject;
            currentLevel.SetActive(true);
            minigameLevels[levelReached].GetComponent<MGLevel>().StartGame();
        }

        private void CloseMinigame()
        {
            // // Switch back to gameplay inputs
            // inputs.SetGameplay();

            // // Go back to gameplay camera
            // CameraManager.Instance.RemoveCamera(cam);
            // CameraManager.Instance.SwitchToMainCamera();
        }
        // TODO: Show appropriate level
        // TODO: Play the selected level
    }
}
