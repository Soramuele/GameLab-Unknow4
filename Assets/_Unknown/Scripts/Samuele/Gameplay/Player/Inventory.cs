using System.Collections.Generic;
using UnityEngine;

namespace Unknown.Samuele
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        private Dictionary<Item, int> inventoryItems = new();

        void Awake()
        {
            Instance = this;
        }

        public void AddItem(Item item)
        {
            if (inventoryItems.ContainsKey(item))
                inventoryItems[item]++;
            else
                inventoryItems.Add(item, 1);
        }

        public void RemoveItem(Item item)
        {
            if (!inventoryItems.ContainsKey(item))
                return;

            if (inventoryItems[item] - 1 == 0)
                inventoryItems.Remove(item);
            else
                inventoryItems[item]--;
        }

        public void RemoveItem(string item)
        {
            foreach (var _item in inventoryItems)
            {
                var _itemKey = _item.Key;

                if (_itemKey.ID.Contains(item, System.StringComparison.OrdinalIgnoreCase))
                    RemoveItem(_itemKey);
            }
        }

        public bool ContainsItem(Item item) =>
            inventoryItems.ContainsKey(item);

        public bool ContainsItem(string item)
        {
            foreach (var _item in inventoryItems)
            {
                var _itemKey = _item.Key;

                if (_itemKey.ID.Equals(item, System.StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
