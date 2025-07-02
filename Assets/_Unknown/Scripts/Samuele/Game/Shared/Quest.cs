using UnityEngine;

namespace Unknown.Samuele
{
    [CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Quest")]
    public class Quest : ScriptableObject
    {
        [Header("Quest info")]
        [SerializeField] private string title;
        [SerializeField, TextArea] private string description;

        [Header("Requirement")]
        [SerializeField] private Item[] itemsToPickup;
        [SerializeField] private float failTime = -1;

        public string Title => title;
        public string Description => description;
        public Item[] ItemsToPickup => itemsToPickup;
        public int Amount => itemsToPickup.Length;
        public float FailTime => failTime;
    }
}
