using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float distance;

    public LayerMask interactionmask;

    private Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray rayy = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitItem;

        Debug.DrawRay(rayy.origin, rayy.direction * 5);

        if (Physics.Raycast(rayy, out hitItem, distance, interactionmask))
        {
            if (hitItem.transform.tag == "interaction")
            {
                Unknown.Samuele.InteractMessage.Instance.UpdateText("Interact");
               
            }

        }
        else
            Unknown.Samuele.InteractMessage.Instance.UpdateText(string.Empty);
    }
}
