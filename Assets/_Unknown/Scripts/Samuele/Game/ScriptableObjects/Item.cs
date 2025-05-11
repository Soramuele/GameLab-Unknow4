using UnityEngine;

namespace Unknown.Samuele
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
    public class Item : ScriptableObject
    {
        public string id;
        public string description;
    }
}
