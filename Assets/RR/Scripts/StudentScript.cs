using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Unknown.Samuele;

public class StudentScript : MonoBehaviour
{
    public Volume temperature;
    [SerializeField] CharacterController characterController;
    public WindowMechanic window;
    private float currentWeight = 0f;
    private float transitionSpeed = 0.1f;
    public GameObject chairtosit;
    public GameObject panel;
    public CinemachineVirtualCamera virtualCamera;
    public CanvasGroup fade;
    public GameObject standuppos;
    public TextMeshProUGUI hint;
    private Unknown.Samuele.StimuliManager stimuli;
    private float something = 0;
    public Image fire;
    private bool isPanelActive = false;

    void Start()
    {
        stimuli = Unknown.Samuele.StimuliManager.Instance;
    }

    void Update()
    {
        temperature.weight = currentWeight;

        SitOnChair();
        StandUP();

        
        if(hint.enabled)
        {
            
        }
        
        if (!panel.activeSelf)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        HandleWeightTransition();
    }

    private void HandleWeightTransition()
    {
      
        if (!window.windowanimator.GetBool("WindowOn") && currentWeight < 1f )
        {
            if (something + 0.07f > 100)
                something = 100;
                
            else
                something += 0.07f;
            
            stimuli.SubscribeDamagePercentage(this.gameObject, something);
            
            if (currentWeight < 1f && fade.alpha < 1 && stimuli.Ratio > 10)
            {
                currentWeight += transitionSpeed * Time.deltaTime;
                if (currentWeight > 1f) currentWeight = 1f;
                fade.alpha += transitionSpeed * Time.deltaTime;
            }
        }
        else if (window.windowanimator.GetBool("WindowOn") )
        {
            hint.enabled = false;
            fire.enabled = false;


            if (something - 0.02f < 0)
                something = 0;
            else
                something -= 0.02f;
            
            stimuli.SubscribeDamagePercentage(this.gameObject, something);
            if (currentWeight > 0f && fade.alpha > 0 && stimuli.Ratio < 70)
            {
                currentWeight -= transitionSpeed * Time.deltaTime;
               
                if (currentWeight < 0f) currentWeight = 0f;
                fade.alpha -= transitionSpeed * Time.deltaTime;
            }
        }

        
      if(something >= 70 && !window.windowanimator.GetBool("WindowOn"))
        {

            hint.enabled = true;
            fire.enabled = true;

        }





    }

    public void SitOnChair()
    {
        
        if (Vector3.Distance(transform.position, chairtosit.transform.position) < 2f && Input.GetKeyDown(KeyCode.E)) 
        {
            characterController.enabled = false;
            Transform sitTransform = chairtosit.transform.GetChild(0);
            transform.position = sitTransform.position;
            transform.rotation = sitTransform.rotation;
            transform.SetParent(chairtosit.transform, true);
            GetComponent<PlayerController>().enabled = false;
            virtualCamera.GetComponent<CinemachineInputProvider>().enabled = false;

            if (!isPanelActive)
            {
                panel.SetActive(true);  
                isPanelActive = true;    
            }

        }
    }

    public void StandUP()
    {
        if (Input.GetKeyDown(KeyCode.G) && !characterController.enabled)
        {
           
            transform.position = standuppos.transform.position;
            transform.rotation = standuppos.transform.rotation;

            virtualCamera.GetComponent<CinemachineInputProvider>().enabled = true;
            characterController.enabled = true;
            GetComponent<PlayerController>().enabled = true;
            transform.SetParent(null);
            if (isPanelActive)
            {
                panel.SetActive(false); 
                isPanelActive = false;  
            }
        }




    }
}
