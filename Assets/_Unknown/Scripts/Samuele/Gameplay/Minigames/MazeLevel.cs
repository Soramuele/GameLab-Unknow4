using UnityEngine;

namespace Unknown.Samuele
{
    public class MazeLevel : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private GameObject player;

        [Header("Blocks")]
        [SerializeField] private GameObject startBlock;

        [Header("VFX")]
        [SerializeField] private ParticleSystem spawnParticle;
        [SerializeField] private ParticleSystem dieParticle;

        
    }
}
