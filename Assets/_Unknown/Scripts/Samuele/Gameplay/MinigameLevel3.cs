using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unknown.Samuele
{
    public class MinigameLevel3 : MonoBehaviour
    {
        [Header("Inputs")]
        [SerializeField] private Inputs.InputHandler inputs;

        [Header("Screens")]
        [SerializeField] private GameObject startScreen;
        [SerializeField] private GameObject endScreen;

        [Header("Levels")]
        [SerializeField] private GameObject level1;
        [SerializeField] private GameObject level2;
        [SerializeField] private GameObject level3;

        void Start()
        {
            startScreen.SetActive(true);
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(false);
            endScreen.SetActive(false);
        }

        void OnEnable()
        {
            inputs.OnBackEvent += CloseMinigame;
        }

        void OnDisable()
        {
            inputs.OnBackEvent -= CloseMinigame;
        }

        public void GotoLevel1()
        {
            startScreen.SetActive(false);
            level1.SetActive(true);
        }

        public void GotoLevel2()
        {
            level1.SetActive(false);
            level2.SetActive(true);
        }

        public void GotoLevel3()
        {
            level2.SetActive(false);
            level3.SetActive(true);
        }

        public void EndGame()
        {
            level3.SetActive(false);
            endScreen.SetActive(true);
        }

        private void CloseMinigame()
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(false);

            startScreen.SetActive(true);

            gameObject.SetActive(false);
            CameraManager.Instance.SwitchToMainCamera();
            GameManager.Instance.ChangeInputMap(GameManager.InputMap.Gameplay);
        }

        public void OnBreakEnter()
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(false);
            startScreen.SetActive(true);
        }
    }
}
