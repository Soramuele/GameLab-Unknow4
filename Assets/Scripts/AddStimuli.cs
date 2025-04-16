using UnityEngine;

public class AddStimuli : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private float maxDistance = 20;
    [SerializeField] private float minDistance = 10;

    private GameObject postProcessing;

    private StimuliManager stimuli;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        stimuli = StimuliManager.Instance;
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
            }else
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
    }
}
