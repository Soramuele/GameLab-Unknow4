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
            if (!inventoryItems.ContainsKey(item.ID))
                inventoryItems.Add(item.ID, item);
            else
                Debug.Log("Item already collected");
        }

        public void RemoveItem(Item item)
        {
            if (inventoryItems.ContainsKey(item.ID))
                inventoryItems.Remove(item.ID);
            else
                Debug.Log("Item already removed or never added");
        }

        public bool CheckForItem(Item item) =>
            inventoryItems.ContainsKey(item.ID);
    }
}