using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Unknown.Samuele
{
    public class StimuliManager : MonoBehaviour, IManager
    {
        public static StimuliManager Instance { get; private set; }

        [Header("Stimuli Data")]
        [SerializeField] private float maxStimuli = 250f;

        [Header("Heal")]
        [SerializeField] private float timeBeforeHeal = 2.75f;
        [SerializeField] private float amountOfHeal = 5.5f;

        private float currentStimuli;
        private float timePassed;

        private SerializedDictionary<GameObject, float> damagePercentage = new SerializedDictionary<GameObject, float>();

        public float Ratio { 
            get => 100 - ((currentStimuli / maxStimuli) * 100);
        }

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            
            currentStimuli = maxStimuli;
        }

        void Update()
        {
            // timePassed += Time.deltaTime;

            // if (timePassed >= timeBeforeHeal && currentStimuli < maxStimuli)
            // {
            //     currentStimuli += amountOfHeal;

            //     if (currentStimuli >= maxStimuli)
            //         currentStimuli = maxStimuli;
            // }
        }

        /// <summary> Apply stimuli to the player (in percentage) </summary>
        /// <param name="obj">In order to subscribe, use " this.gameobject " or the GameObject responsable for the damage</param>
        /// <param name="damage">The percentage of stimuli you wish to apply (between 0 and 100)</param>
        public void SubscribeDamagePercentage(GameObject obj, float damage)
        {
            if (damage > 100)
            {
                Debug.LogError("You sure? It's more than the max");
                Debug.Break();
            }

            if (!damagePercentage.ContainsKey(obj))
                damagePercentage.Add(obj, damage);
            else if (damagePercentage[obj] != damage)
                damagePercentage[obj] = damage;
            else
                return;

            CalculateStimuli();
        }

        /// <summary> Remove stimuli from the player if already subscribed </summary>
        /// <param name="obj">Use " this.gameobject " or the GameObject responsable for the damage</param>
        public void UnsubscribeDamagePercentage(GameObject obj)
        {
            if (damagePercentage.ContainsKey(obj))
                damagePercentage.Remove(obj);

            CalculateStimuli();
        }

        private void CalculateStimuli()
        {
            var damage = 0f;

            foreach(var item in damagePercentage.Values)
            {
                var dmg = (item / 100) * maxStimuli;
                
                damage += dmg;
            }

            currentStimuli = maxStimuli - damage;
        }

        #region Manager
        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public void Load()
        {
            damagePercentage.Clear();
            throw new System.NotImplementedException();
        }
    #endregion
    }
}
