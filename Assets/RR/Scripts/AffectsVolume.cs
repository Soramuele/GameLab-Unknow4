using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using static System.TimeZoneInfo;

public class AffectsVolume : MonoBehaviour
{
    public Volume volume;           
    private UnityEngine.Rendering.Universal.Vignette vignette;      
    private float targetIntensity;  
    private float currentIntensity;  
    private float transitionSpeed = 1f;  
    private float transitionTime = 0;
    private float delayTime = 2;



    private float timeToFadeIn = 6f;  
    private float timeToFadeOut = 2f;

    void Start()
    {
      
        if (volume.profile.TryGet<UnityEngine.Rendering.Universal.Vignette>(out vignette))
        {
            currentIntensity = vignette.intensity.value;  
            targetIntensity = currentIntensity;           
        }


        if (volume.profile.TryGet<UnityEngine.Rendering.Universal.Vignette>(out vignette))
        {
            transitionSpeed = 1f;  // Speed of intensity change
            targetIntensity = 0f;  // Start at intensity 0
            
        }
    }

    void Update()
    {
        // If we're in the delay period, just count down the delay
        if (delayTime > 0f)
        {
            delayTime -= Time.deltaTime;  // Decrease delay time
            return;  // Skip the transition logic until delay is finished
        }

        // If we're transitioning from 0 to 1 (fade-in)
        if (targetIntensity == 0f && transitionTime < timeToFadeIn)
        {
            vignette.intensity.value = Mathf.Lerp(0f, 1f, transitionTime / timeToFadeIn);  // Lerp from 0 to 1
        }
        // If we're transitioning from 1 to 0 (fade-out)
        else if (targetIntensity == 1f && transitionTime < timeToFadeOut + timeToFadeIn)
        {
            vignette.intensity.value = Mathf.Lerp(1f, 0f, (transitionTime - timeToFadeIn) / timeToFadeOut);  // Lerp from 1 to 0
        }

        // If the fade-in transition (0 to 1) is completed, switch to fade-out (1 to 0)
        if (transitionTime >= timeToFadeIn && targetIntensity == 0f)
        {
            targetIntensity = 1f;  // Switch to fade-out (1)
            transitionTime = 0f;   // Reset transition time
        }
        // If the fade-out transition (1 to 0) is completed, reset delay before next transition
        else if (transitionTime >= timeToFadeOut + timeToFadeIn && targetIntensity == 1f)
        {
            targetIntensity = 0f;  // Switch to fade-in (0)
            transitionTime = 0f;   // Reset transition time

            // Reset delay time to wait before starting the next cycle
            delayTime = 2;
        }

        // Increment transition time to keep track of the transitions
        transitionTime += Time.deltaTime;
    }
}
