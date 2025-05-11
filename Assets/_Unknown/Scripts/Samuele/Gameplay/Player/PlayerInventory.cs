using System.Collections.Generic;
using UnityEngine;

namespace Unknown.Samuele
{
    public class PlayerInventory : MonoBehaviour
    {
        public static PlayerInventory Instance { get; private set; }

        private Dictionary<string, Item> inventoryItems;

        void Awake()
        {
            Instance = this;

            inventoryItems = new Dictionary<string, Item>();
        }

        public void AddItem(Item item)
        {
            if (!inventoryItems.ContainsKey(item.id))
                inventoryItems.Add(item.id, item);
        }

        public void RemoveItem(Item item)
        {
            if (inventoryItems.ContainsKey(item.id))
                inventoryItems.Remove(item.id);
        }

        public bool CheckForItem(Item item) =>
            inventoryItems.ContainsKey(item.id);
    }
}
