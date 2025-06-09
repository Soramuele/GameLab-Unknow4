using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Unknown.Samuele;
using static System.TimeZoneInfo;

public class Level4 : MonoBehaviour
{
    public Volume temperature333;
    private float currentWeight = 0f;
    private float transitionSpeed = 0.25f;
    private float something = 0;
    private float transitionTime = 0;
    private Unknown.Samuele.StimuliManager stimuli;
    public TextMeshProUGUI hintik; //moveaway
    public TextMeshProUGUI globalhint;
    public TextMeshProUGUI textik;
    public GameObject student;
    public GameObject finishwall1;
    public GameObject finishwall2;
    public GameObject finishdoor;
    public GameObject finishdoor2;
    public FloppyManager playing;
    // Start is called before the first frame update
    void Start()
    {
        stimuli = Unknown.Samuele.StimuliManager.Instance;

    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        transitionTime += Time.deltaTime;
        temperature333.weight = currentWeight;

        if(playing.isPlaying)
        {


            textik.enabled = true;

        }

        else
        {
            textik.enabled = false;
        }

        if(playing.isPlaying)
        {

            globalhint.text = "get 35 points";

        }

        Stimuli();
        Finishingeeg();
    }


    private void Stimuli()
    {
        if (Vector3.Distance(this.transform.position, student.transform.position) < 2 && playing.isPlaying && currentWeight < 1)

        {
            if (something + 0.2f > 100)
            {
                something = 100;
                transitionTime = 0;
                
            }

            else
                something += 0.2f;


            
            stimuli.SubscribeDamagePercentage(this.gameObject, something);

            if (currentWeight < 1f && stimuli.Ratio > 10)
            {
                currentWeight += transitionSpeed * Time.deltaTime;



            }

        }

       

      else  if(Vector3.Distance(this.transform.position, student.transform.position) > 5 && !playing.isPlaying)
        {

            if (something - 0.2f > 100)
            {
                something = 0;
                transitionTime = 0;

            }

            else
                something -= 0.2f;

            stimuli.SubscribeDamagePercentage(this.gameObject, something);


            if (currentWeight > 0f && stimuli.Ratio < 70)
            {
                currentWeight -= transitionSpeed * Time.deltaTime;

                if (currentWeight < 0f) currentWeight = 0f;
                
            }

        }

        if (stimuli.currentStimuli < 70)
        {

            hintik.enabled = true;

        }

        else { hintik.enabled = false; }
    }    


    private void Finishingeeg()
    {

            if (playing.record >= 30)
            {
            globalhint.text = "Leave the building";
               
             finishdoor.SetActive(true);
             finishdoor2.SetActive(true);

            }

            if(Vector3.Distance(this.transform.position, finishdoor.transform.position) < 3 && Input.GetKeyDown(KeyCode.E))
                {


            SceneManager.LoadScene("TitleScreen");

                }


        if (Vector3.Distance(this.transform.position, finishdoor2.transform.position) < 3 && Input.GetKeyDown(KeyCode.E))
        {


            SceneManager.LoadScene("TitleScreen");

        }





    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("NextLevel"))
        {
            globalhint.text = "OPEN LAPTOP";
            finishwall1.SetActive(false);
            finishwall2.SetActive(false);

        }



        if (other.gameObject.tag == "End")
        {

            SceneManager.LoadScene("TitleScreen");

        }
    }



  
}
