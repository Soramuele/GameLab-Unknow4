using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessManager : MonoBehaviour
{
    public static BrightnessManager Instance;

    [Header("Light Settings")]
    [SerializeField] private float maxIntensity = 100;
    [SerializeField] private float intensity = 1;
    
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

        lights.Clear();
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
    }

    void OnValidate()
    {
        if (lights.Count == 0)
            Start();
        
        if (lights.Count >= 1)
            foreach(var _light in lights)
                _light.light.intensity = intensity;
    }

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
                    _light.light.intensity = intensity += 0.1f;
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
                    _light.light.intensity = intensity -= 0.1f;
            }
            yield return new WaitForEndOfFrame();
        }

        isSubtracting = false;
        alreadySubtracted = true;
    }
}
