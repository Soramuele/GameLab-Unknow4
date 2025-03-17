using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessManager : MonoBehaviour
{
    [Header("Light Settings")]
    [SerializeField, Range(0, 50)] private float intensity = 1;
    [SerializeField] private float maxIntensity = 50;

    [Header("Duration")]
    [SerializeField, Range(0, 1)] private float time = .1f;
    
    [Serializable]
    private class Lightning
    {
        public Light light;
        public float intensity;
    }

    private List<Lightning> lights;

    private bool isAdding = false;
    private bool isSubtracting = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var _lights in FindObjectsOfType<Light>())
        {
            var lightning = new Lightning() {
                light = _lights,
                intensity = _lights.intensity
            };

            lights.Add(lightning);
        }
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
        if (!isSubtracting)
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

        while(intensity < maxIntensity)
        {
            intensity -= .1f;
            foreach(var _light in lights)
            {
                if (_light.light.intensity > _light.intensity)
                _light.light.intensity -= .1f;
            }
            yield return new WaitForSeconds(time);
        }

        isAdding = false;
    }
    
    private IEnumerator ISubtractBrightness()
    {
        isSubtracting = true;

        while(intensity > 1)
        {
            intensity -= .1f;
            foreach(var _light in lights)
            {
                if (_light.light.intensity > _light.intensity)
                _light.light.intensity -= .1f;
            }
            yield return new WaitForSeconds(time);
        }

        isSubtracting = false;
    }
}
