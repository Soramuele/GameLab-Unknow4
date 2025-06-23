using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle vsyncToggle;
    [SerializeField] private TMP_Dropdown qualityDropdown;

    // Start is called before the first frame update
    void Start()
    {
        fullscreenToggle.isOn = Screen.fullScreen;
        vsyncToggle.isOn = QualitySettings.vSyncCount == 1 ? true : false;
        qualityDropdown.value = QualitySettings.GetQualityLevel();

        fullscreenToggle.onValueChanged.AddListener(ChangeFullscreen);
        vsyncToggle.onValueChanged.AddListener(ChangeVsync);
        qualityDropdown.onValueChanged.AddListener(ChangeQualityLevel);
    }

    private void ChangeFullscreen(bool value)
    {
        Screen.fullScreen = value;
    }

    private void ChangeVsync(bool value)
    {
        QualitySettings.vSyncCount = value ? 1 : 0;
    }

    private void ChangeQualityLevel(int value)
    {
        QualitySettings.SetQualityLevel(value);
    }
}
