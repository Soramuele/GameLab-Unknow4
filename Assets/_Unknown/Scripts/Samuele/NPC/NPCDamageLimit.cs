using System.Collections.Generic;
using UnityEngine;

namespace Unknown.Samuele
{
    public class NPCDamageLimit : MonoBehaviour
    {
        private static List<NPCLooking> npcDamage = new List<NPCLooking>();
        public static int maxEnemyDamage = 3;

        private bool damageFrame = true;
        
        // Update is called once per frame
        void Update()
        {
            if (!damageFrame)
                npcDamage.Clear();

            damageFrame = !damageFrame;
        }

        public static bool TryRegister(NPCLooking npc)
        {
            if (npcDamage.Count >= maxEnemyDamage)
                return false;
            
            npcDamage.Add(npc);
            return true;
        }
    }
}
