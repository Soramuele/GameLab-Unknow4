using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator dooranimation;
    public GameObject door;
    public MiniGameScript gameScript;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameScript.Endscreen.activeSelf && Vector3.Distance(this.transform.position, door.transform.position) < 3f && Input.GetKeyDown(KeyCode.E)) 
        {

            dooranimation.enabled = true;
        
        
        
        
        }



    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NextLevel") && dooranimation.enabled)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
