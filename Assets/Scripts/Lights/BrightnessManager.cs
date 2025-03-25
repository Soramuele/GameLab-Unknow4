using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessManager : MonoBehaviour
{
    public static BrightnessManager Instance;

    [Header("Light Settings")]
    [SerializeField] private float intensity = 1;
    [SerializeField] private float maxIntensity = 100;
    private float intensityIncrement = 0.2f;
    
    [Serializable]
    private class Lightning
    {
        public Light light;
        public float intensity;
    }

    private List<Lightning> lights;

    private bool isAdding = false;
    private bool isSubtracting = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        lights = new();

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

#if UNITY_EDITOR
    void OnValidate()
    {
        Start();
        
        // foreach(var _light in lights)
        // {
        //     // Debug.Log(_light);
        //     try{
        //         _light.light.intensity = intensity;
        //     }
        //     catch(Exception e)
        //     {
        //         Debug.Log(e.Message);
        //     }
        // }

        for (int i = 0; i < lights.Count; i++)
            try{
                lights[i].light.intensity = intensity;
            }
            catch(Exception e)
            {
                Debug.Log(i + "\n" + e.Message);
            }
    }
#endif

    public void AddBrightness()
    {
        if (!isAdding)
        {
            Debug.LogWarning("Adding");
            if (isSubtracting)
            {
                StopCoroutine(ISubtractBrightness());
                isSubtracting = false;
            }

            StartCoroutine(IAddBrightness());
        }
    }

    public void SubtractBrightness()
    {
        if (!isSubtracting && intensity > 1)
        {
            Debug.LogWarning("Subtracting");
            if (isAdding)
            {
                StopCoroutine(IAddBrightness());
                isAdding = false;
            }

            StartCoroutine(ISubtractBrightness());
        }
    }

    private IEnumerator IAddBrightness()
    {
        isAdding = true;

        while (intensity < maxIntensity)
        {
            intensity += intensityIncrement;
            foreach (var _light in lights)
            {
                _light.light.intensity += intensityIncrement;
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
            intensity = intensity - intensityIncrement < 1 ? 1 : intensity - intensityIncrement;
            foreach(var _light in lights)
            {
                if (_light.light.intensity > _light.intensity)
                    if (_light.light.intensity - intensityIncrement < _light.intensity)
                        _light.light.intensity = _light.intensity;
                    else
                        _light.light.intensity -= intensityIncrement;
            }
            yield return new WaitForEndOfFrame();
        }

        isSubtracting = false;
    }
}
