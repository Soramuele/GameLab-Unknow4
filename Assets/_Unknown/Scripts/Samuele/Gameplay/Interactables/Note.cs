using UnityEngine;

namespace Unknown.Samuele
{
    public class Note : Interactable
    {
        [Header("Item")]
        [SerializeField] private Item item;

        [Header("Audio")]
        [SerializeField] private SOAudio pickupAudio;

        protected override void Interaction()
        {
            var _audio = pickupAudio.SFXClips["pickup"][0];

            AudioManager.Instance.PlaySFX(_audio, transform.position);

            Inventory.Instance.AddItem(item);

            Destroy(gameObject);
        }
    }
}
