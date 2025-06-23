using UnityEngine;
using UnityEngine.UI;

namespace Unknown.Samuele
{
    public class StimuliUI : MonoBehaviour
    {
        [Header("Bar")]
        [SerializeField] private Image stimuliBar;

        private StimuliManager stimuliManager;

        void Start()
        {
            stimuliBar.fillAmount = 0;
        }

        void OnEnable()
        {
            stimuliManager = StimuliManager.Instance;

            stimuliManager.OnStimuliChangedEvent += UpdateStimuliBar;
        }

        void OnDisable()
        {
            stimuliManager.OnStimuliChangedEvent -= UpdateStimuliBar;
        }

        private void UpdateStimuliBar(float value)
        {
            stimuliBar.fillAmount = value;

            stimuliBar.color = Color.Lerp(Color.white, Color.red, stimuliBar.fillAmount);
        }
    }
}
