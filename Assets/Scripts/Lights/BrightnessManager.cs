using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessManager : MonoBehaviour
{
    public static BrightnessManager Instance;

    [Header("Light Settings")]
    [SerializeField, Range(0, 100)] private float intensity = 1;
    [SerializeField] private float maxIntensity = 100;

    // [Header("Duration")]
    // [SerializeField, Range(0, 1)] private float time = .1f;
    
    [Serializable]
    private class Lightning
    {
        public Light light;
        public float intensity;
    }

    private List<Lightning> lights = new();

    private bool isAdding = false;
    private bool isSubtracting = false;
    private bool alreadySubtracted = true;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var _lights in FindObjectsOfType<Light>())
        {
            var myLight = _lights.GetComponent<Light>();
            var lightning = new Lightning() {
                light = myLight,
                intensity = myLight.intensity
            };

            lights.Add(lightning);
        }

        StartCoroutine(IAddBrightness());
    }

    // void OnValidate()
    // {
    //     Start();
    //     foreach (var _light in lights)
    //         _light.light.intensity = intensity;
    // }

    public void AddBrightness()
    {
        if (!isAdding)
        {
            if(isSubtracting)
                StopCoroutine(ISubtractBrightness());
            isSubtracting = false;

            StartCoroutine(IAddBrightness());
        }
    }

    public void SubtractBrightness()
    {
        if (!isSubtracting && !alreadySubtracted)
        {
            if(isAdding)
                StopCoroutine(IAddBrightness());
            isAdding = false;

            StartCoroutine(ISubtractBrightness());
        }
    }

    private IEnumerator IAddBrightness()
    {
        isAdding = true;
        alreadySubtracted = false;

        while(intensity < maxIntensity)
        {
            foreach(var _light in lights)
            {
                if (_light.light.intensity >= _light.intensity)
                _light.light.intensity = ++intensity;
            }
            yield return new WaitForEndOfFrame();
        }

        isAdding = false;
    }
    
    private IEnumerator ISubtractBrightness()
    {
        isSubtracting = true;

        while(intensity > 1)
        {
            foreach(var _light in lights)
            {
                if (_light.light.intensity > _light.intensity)
                _light.light.intensity = --intensity;
            }
            yield return new WaitForEndOfFrame();
        }

        isSubtracting = false;
        alreadySubtracted = true;
    }
}
