using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator dooranimation;
    public GameObject door;
    public MiniGameScript gameScript;
    public GameObject somethingPleaseWork;

    void Start()
    {
        if (!somethingPleaseWork.TryGetComponent<MiniGameScript>( out gameScript ) )
        {
            Debug.LogError("Can't find it!");

            Debug.Break();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameScript.Endscreen.activeSelf && Vector3.Distance(this.transform.position, door.transform.position) < 3f && Input.GetKeyDown(KeyCode.E)) 
        {
            
            dooranimation.enabled = true;
            

        }

        if(gameScript.Endscreen.activeSelf && this.transform.parent == null) 
        {

            gameScript.hintext.text = "leave the classroom";
        
        
        
        }

    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NextLevel") && dooranimation.enabled)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            ///change the scene
        }
    }
}
