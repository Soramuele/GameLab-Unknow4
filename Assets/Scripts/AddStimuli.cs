using UnityEngine;

public class AddStimuli : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private float maxDistance = 20;
    [SerializeField] private float minDistance = 10;

    private GameObject postProcessing;

    private Unknown.Samuele.StimuliManager stimuli;
    private GameObject player;
    public PlayerController playerok;
    // Start is called before the first frame update
    void Start()
    {
        stimuli = Unknown.Samuele.StimuliManager.Instance;
        player = FindObjectOfType<PlayerController>().gameObject;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        postProcessing = GameObject.FindGameObjectWithTag("Volume");
    }

    // Update is called once per frame
    void Update()
    {
        var _dist = Vector3.Distance(transform.position, player.transform.position);
        if (_dist <= maxDistance)
        {
            if (_dist <= minDistance){
                stimuli.SubscribeDamagePercentage(gameObject, 50);
                postProcessing.SetActive(true);
               

            }
            else
            {
                var _range = maxDistance - minDistance;
                var _pos = maxDistance - _dist;
                var _damage = ((100 / _range) * _pos) / 2;
                
                stimuli.SubscribeDamagePercentage(gameObject, _damage);
              
            }
        }
        else
        {
            stimuli.UnsubscribeDamagePercentage(gameObject);
            postProcessing.SetActive(false);
        }

       if(stimuli.currentStimuli <= 80)
        {
            playerok.playerSpeed = 0.5f;

        }

        else { playerok.playerSpeed = 2; }
        
        
    }
}
