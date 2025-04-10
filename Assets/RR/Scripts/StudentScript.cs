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
            // Disable CharacterController to allow manual positioning
            characterController.enabled = false;

            // Get the first child of the chair — your sit position
            Transform sitTransform = chairtosit.transform.GetChild(0);

            // Move player to that position and rotation
            transform.position = sitTransform.position;
            transform.rotation = sitTransform.rotation;

            // Optionally parent the player to the chair (so they move with it)
            transform.SetParent(chairtosit.transform, true);

            GetComponent<PlayerController>().enabled = false;
        }
    }



    public void StandUP()
    {


        if (Input.GetKeyDown(KeyCode.Space) && !characterController.enabled)
        {
            // Re-enable character controller and movement
            characterController.enabled = true;
            GetComponent<PlayerController>().enabled = true;
  

            // Unparent from the chair
            transform.SetParent(null);

            // Optional: Small upward push so they don't clip into the chair
            transform.position += Vector3.up * 0.1f;
        }



    }

}
