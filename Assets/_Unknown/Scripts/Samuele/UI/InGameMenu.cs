using UnityEngine;

namespace Unknown.Samuele
{
    public class InGameMenu : MenuHandler
    {
        [Header("Menu")]
        [SerializeField] private Canvas pauseMenu;

        private GameManager gameManager;

        protected override void Start()
        {
            base.Start();

            pauseMenu.gameObject.SetActive(false);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            gameManager = GameManager.Instance;

            gameManager.OnPauseEvent += Pause;
            gameManager.OnResumeEvent += Resume;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            gameManager.OnPauseEvent -= Pause;
            gameManager.OnResumeEvent -= Resume;
        }

        private void Pause()
        {
            pauseMenu.gameObject.SetActive(true);
        }

        private void Resume()
        {
            pauseMenu.gameObject.SetActive(false);
        }

        public void OnResume()
        {
            gameManager.ResumeFromSettings();
        }

        public void OnQuit()
        {
            gameManager.BackToTitleScreen();
        }
    }
}
