using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class StudentScript : MonoBehaviour
{

    public Volume temperature;
    public WindowMechanic window;
    private float currentWeight = 0f;
    private float transitionSpeed = 0.08f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        temperature.weight = currentWeight;


        if (window.windowanimator.GetBool("WindowOff") && currentWeight < 1f)
        {
            if (currentWeight < 1f)
            {
                currentWeight += transitionSpeed * Time.deltaTime;
                if (currentWeight > 1f) currentWeight = 1f;
            }
        }
        else if (!window.windowanimator.GetBool("WindowOff") && currentWeight > 0f)
        {

            if (currentWeight > 0f)
            {
                currentWeight -= transitionSpeed * Time.deltaTime;
                if (currentWeight < 0f) currentWeight = 0f;
            }


        }
    }
}
