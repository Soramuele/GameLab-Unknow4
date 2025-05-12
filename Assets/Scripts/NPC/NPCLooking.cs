using UnityEngine;

public class NPCLooking : MonoBehaviour
{
    // DOT MACROS
    private const float DOT_30Degrees = 0.866f;
    // private const float DOT_15Degrees = 0.966f;
    // private const float DOT_10Degrees = 0.978f;

    [Header("NPC Data")]
    [SerializeField, Range(1, 50)] private float damage = 5f;
    [SerializeField] private GameObject NPCEyes;

    [Header("Player Distance")]
    [SerializeField] private float playerDistance = 15f;

    private GameObject player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    void Update()
    {
        // Check if player in sight
        if (Vector3.Distance(player.transform.position, transform.position) <= playerDistance)
            StimulatesThePlayer();
    }
    
    private void StimulatesThePlayer()
    {
        var _playerSightDir = Camera.main.transform.forward;

        // Check if player is looking towards npc
        var _dotResult = - Vector3.Dot(_playerSightDir, NPCEyes.transform.forward);
        
        var stimuli = Unknown.Samuele.StimuliManager.Instance;
        // Damage the player based on how much he's looking
        if (_dotResult >= DOT_30Degrees)
        {
            Debug.Log("Damage");
            stimuli.SubscribeDamagePercentage(gameObject, damage);
        }
        else
        {
            Debug.Log("No damage");
            stimuli.UnsubscribeDamagePercentage(gameObject);
        }
    }
}
