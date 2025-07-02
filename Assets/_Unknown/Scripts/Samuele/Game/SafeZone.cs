using UnityEngine;

namespace Unknown.Samuele
{
    public class SafeZone : MonoBehaviour
    {
        private StimuliManager stimuliManager;

        void Start()
        {
            stimuliManager = StimuliManager.Instance;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                stimuliManager.SetSafeZone(true);
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                stimuliManager.SetSafeZone(false);
        }
    }
}
