using UnityEngine;

namespace Unknown.Samuele
{
    public class ComputerInteractable : Interactable
    {
        [Header("Inputs")]
        [SerializeField] private InputHandler inputs;

        [Header("Minigame")]
        [SerializeField] private Canvas minigameCanvas;

        protected override void Interaction()
        {
            // Open Minigame
            var minigame = minigameCanvas.gameObject;
            minigame.SetActive(true);
            minigame.GetComponent<MinigameHandler>().SetupMinigame();

            // Switch input map
            inputs.SetMinigame();
        }
    }
}
