using UnityEngine;

namespace Unknown.Samuele
{
    public class DamageSource : MonoBehaviour
    {
        [Header("Damage")]
        [SerializeField] private float damagePerSecond = 3f;
        [SerializeField] private float maxDamage = 50f;

        private float damageDealt = 0f;

        private StimuliManager stimuliManager;

        // Getters
        public float DamageDealt => damageDealt;

        void Start()
        {
            stimuliManager = StimuliManager.Instance;
        }

        public void StimulatePlayer(float deltaTime)
        {
            var damage = damagePerSecond * deltaTime;

            if (damageDealt >= maxDamage)
                damage = 0f;

            stimuliManager.ApplyDamage(this, damage, out var canDamage);

            if (!canDamage)
                return;

            damageDealt = Mathf.Min(damageDealt + damage, maxDamage);
        }

        public void ReduceTotalStimuliDealt(float healingAmount)
        {
            damageDealt = Mathf.Max(0f, damageDealt - healingAmount);

            if (damageDealt == 0f)
                stimuliManager.RemoveSource(this);
        }
    }
}
