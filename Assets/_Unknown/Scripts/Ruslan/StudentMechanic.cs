using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Unknown.Ruslan
{
    public class StudentMechanic : MonoBehaviour
    {
        // Start is called before the first frame update

        public GameObject Window;
        private NavMeshAgent Agent;
        public WindowMechanic windowscript;
        public GameObject startposition;
        public Animator animatorwalk;
        public float seconds;
    

        



        void Start()
        {

            Agent = GetComponent<NavMeshAgent>();
        
        }

        // Update is called once per frame
        void Update()
        {
            seconds += Time.deltaTime;
            WindowClosing();

        }

        public void WindowClosing()
        {

            if(seconds > 15 && windowscript.windowanimator.GetBool("WindowOn"))
            
            {
                Agent.enabled = true;
                Agent.SetDestination(Window.transform.position);
                animatorwalk.SetBool("Walk", true);
            
            
            }

        

            if (Vector3.Distance(transform.position, Window.transform.position) < 2.5f)
            {
                windowscript.windowanimator.SetBool("WindowOn", false);
                Agent.SetDestination(startposition.transform.position);
                animatorwalk.SetBool("Walk", true);
                seconds = 0;

            

            }

            if (Vector3.Distance(transform.position, startposition.transform.position) < 1.5f && !windowscript.windowanimator.GetBool("WindowOn"))
            {

                animatorwalk.SetBool("Walk", false);

            }

        }

    }
}
