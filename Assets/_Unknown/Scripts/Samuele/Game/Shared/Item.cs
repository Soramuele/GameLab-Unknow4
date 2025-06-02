using UnityEngine;

namespace Unknown.Samuele
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField, TextArea] private string description;

        public string ID => id;
        public string Description => description;
    }
}
