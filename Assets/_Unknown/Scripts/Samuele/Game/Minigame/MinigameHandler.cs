using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unknown.Samuele
{
    public class MinigameHandler : MonoBehaviour
    {
        [Header("Inputs")]
        [SerializeField] private InputHandler inputs;

        [Header("PLayer")]
        [SerializeField] private MinigamePlayerControls player;
        [SerializeField] private CinemachineVirtualCamera cam;
        [SerializeField] private Item reward;

        [Header("Screens")]
        [SerializeField] private GameObject mainScreen;
        [SerializeField] private GameObject endScreen;
        [SerializeField] private MGLevel[] minigameLevels;

        private GameObject currentLevel;
        private int levelReached = 0;

        void Start()
        {
            mainScreen.SetActive(true);
            endScreen.SetActive(false);
            foreach(var level in minigameLevels)
                level.gameObject.SetActive(false);

            CameraManager.Instance.SwitchCamera(cam);
            GameManager.Instance.GetGameplay().IsMinigameOn = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        void OnEnable()
        {
            CameraManager.Instance.BlendingCompleteEvent += () => CameraManager.Instance.SwitchCamera(cam);
            inputs.BackEvent += CloseMinigame;
            player.FinishReachedEvent += NextLevel;
        }
        
        void OnDisable()
        {
            CameraManager.Instance.BlendingCompleteEvent -= () => CameraManager.Instance.SwitchCamera(cam);
            inputs.BackEvent -= CloseMinigame;
            player.FinishReachedEvent -= NextLevel;
        }

        public void StartMinigame()
        {
            mainScreen.SetActive(false);

            Cursor.visible = false;
            
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
                currentLevel.SetActive(false);
                levelReached = 0;
                currentLevel = null;
                endScreen.SetActive(true);
                PlayerInventory.Instance.AddItem(reward);
            }
        }

        private void SetupLevel()
        {
            currentLevel = minigameLevels[levelReached].gameObject;
            currentLevel.SetActive(true);
            currentLevel.GetComponent<MGLevel>().StartGame();
        }

        private void CloseMinigame()
        {
            // Switch back to gameplay inputs
            inputs.SetGameplay();

            // Go back to gameplay camera
            CameraManager.Instance.RemoveCamera(cam);
            CameraManager.Instance.SwitchToMainCamera();

            GameManager.Instance.GetGameplay().IsMinigameOn = false;

            // Close this scene
            SceneManager.UnloadSceneAsync(3);
        }
        // TODO: Show appropriate level
        // TODO: Play the selected level
    }
}
