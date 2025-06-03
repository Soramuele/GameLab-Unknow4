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
    private float transitionSpeed = 0.25f;
    [SerializeField] CharacterController characterController;
    private Unknown.Samuele.StimuliManager stimuli;
    private float something = 0;
    private float transitionTime = 0;
    public TextMeshProUGUI hintik;
    public PlayerController playerController;
    public GameObject Door;
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
        Finishing();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

       
    }
    private void HandleWeightTransition1()
    {
        if(stimuli.currentStimuli >= 0)
        {
            if (transitionTime > 10 && currentWeight < 1f)

            {
                if (something + 0.2f > 100)
                {
                    something = 100;
                    transitionTime = 0;
                    playerController.playerSpeed = 1f;
                }

                else
                    something += 0.2f;



                stimuli.SubscribeDamagePercentage(this.gameObject, something);

                if (currentWeight < 1f && stimuli.Ratio > 10)
                {
                    currentWeight += transitionSpeed * Time.deltaTime;



                }

            }




        }
            

   
      

        if( something > 70)
        {

            hintik.enabled = true;
            

        }


        if(something <= 0)
        {

            currentWeight = 0;
            playerController.playerSpeed = 2;
        }


        if(something == 100)
        {
            currentWeight = 1;
            playerController.playerSpeed = 1f;
        }
        
        //if (currentWeight > 0f && stimuli.Ratio < 70)
        //{
        //    currentWeight -= transitionSpeed * Time.deltaTime;

        //    if (currentWeight < 0f) currentWeight = 0f;
           
        //}


    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("cHILL") && something > 0)
        {
            print("adaad");
            transitionTime = 0;
            hintik.enabled = false;
            something -= 0.2f;
            currentWeight -= 0.002f;
            

            stimuli.SubscribeDamagePercentage(this.gameObject, something);

         
         

        }
    }

    
    private void Finishing()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(this.transform.position, Door.transform.position) < 3) 
        {

            UnityEditor.EditorApplication.isPlaying = false;


        }

    }
}
