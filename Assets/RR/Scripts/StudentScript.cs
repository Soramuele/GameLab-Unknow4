using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class StudentScript : MonoBehaviour
{

    public Volume temperature;
    [ SerializeField] CharacterController characterController;
    public WindowMechanic window;
    private float currentWeight = 0f;
    private float transitionSpeed = 0.08f;
    public GameObject chairtosit;
    public GameObject panel;
    public CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        temperature.weight = currentWeight;
        SitOnChair();
        StandUP();

        Cursor.visible = false;


       if(!panel.activeSelf) 
        {

            Cursor.lockState = CursorLockMode.Locked;

        }
        



        if (!window.windowanimator.GetBool("WindowOn") && currentWeight < 1f )
        {
            if (currentWeight < 1f)
            {
                currentWeight += transitionSpeed * Time.deltaTime;
                if (currentWeight > 1f) currentWeight = 1f;
            }
        }
        else if (window.windowanimator.GetBool("WindowOn") && currentWeight > 0f )
        {

            if (currentWeight > 0f)
            {
                currentWeight -= transitionSpeed * Time.deltaTime;
                if (currentWeight < 0f) currentWeight = 0f;
            }

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
            panel.SetActive(true);

           
        }


    }



    public void StandUP()
    {


        if (Input.GetKeyDown(KeyCode.Space) && !characterController.enabled)
        {
           
            characterController.enabled = true;
            GetComponent<PlayerController>().enabled = true;
  

           
            transform.SetParent(null);

            transform.position += Vector3.up * 0.1f;
            virtualCamera.GetComponent<CinemachineInputProvider>().enabled = true;
            panel.SetActive(false);
        }



    }

}
