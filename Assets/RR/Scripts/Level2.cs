using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Rendering;

public class Level2 : MonoBehaviour

{
    public Volume temperature;
    private float currentWeight = 0f;
    private float transitionSpeed = 0.4f;
    [SerializeField] CharacterController characterController;
    private Unknown.Samuele.StimuliManager stimuli;
    private float something = 0;
    private float transitionTime = 0;
    public TextMeshProUGUI hintik;

    // Start is called before the first frame update
    void Start()
    {
        stimuli = Unknown.Samuele.StimuliManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        transitionTime += Time.deltaTime;
        temperature.weight = currentWeight;
        HandleWeightTransition1();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        print(transitionTime);
    }
    private void HandleWeightTransition1()
    {

        if(transitionTime > 5 && currentWeight < 1f) 
        
        {
            if (something + 0.02f > 100)
            {
                something = 100;
                transitionTime = 0;

            }
                
       

          
            else
                something += 0.1f;
  

            stimuli.SubscribeDamagePercentage(this.gameObject, something);

            if (currentWeight < 1f && stimuli.Ratio > 10)
            {
                currentWeight += transitionSpeed * Time.deltaTime;
                if (currentWeight > 1f) currentWeight = 1f;
                
            }




        }

        if( something > 70)
        {

            hintik.enabled = true;

        }
        stimuli.SubscribeDamagePercentage(this.gameObject, something);
        if (currentWeight > 0f && stimuli.Ratio < 70)
        {
            currentWeight -= transitionSpeed * Time.deltaTime;

            if (currentWeight < 0f) currentWeight = 0f;
           
        }


    }


    void OnTriggerEnter(Collider other)
    {
        

        if(other.CompareTag("cHILL") && something > 0)
        {
           
            transitionTime = 0;
            hintik.enabled = false;
            
        }


    }
    
  
    
}
