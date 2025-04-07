using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowMechanic : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator windowanimator;
    public GameObject player;
    public StudentMechanic timer;
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        PlayerOpenWindow();
    }


    private void PlayerOpenWindow()
    {

        if (Vector3.Distance(this.transform.position, player.transform.position) < 3 && Input.GetKeyDown(KeyCode.E))
        {

            windowanimator.SetBool("WindowOn", true);

            timer.seconds = 0;

            //off the effect function
        }


       


    }

   
}
