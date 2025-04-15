using UnityEngine;
namespace Unknown.Samuele
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public float InGameTimer { get; private set; }

        private GameplayManager gameplayManager = new GameplayManager();
        public GameplayManager GetGameplay() =>
            gameplayManager;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            InGameTimer += Time.deltaTime;
        }
    }
}
