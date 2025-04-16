using UnityEngine;
using UnityEngine.UI;

public class StimuliBar : MonoBehaviour
{
    [Header("Bar")]
    [SerializeField] private Image image;

    private StimuliManager stimuli;

    void Start()
    {
        stimuli = StimuliManager.Instance;
    }

    // Update is called once per frame
    void Update() =>
        image.fillAmount = stimuli.Ratio / 100;
}
