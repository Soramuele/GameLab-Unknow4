using UnityEngine;

namespace Unknown.Samuele
{
    public class WindowInteractable : Interactable
    {
        [Header("Data")]
        [SerializeField] private Animator anim;
        [SerializeField] private bool isOpen;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            
            anim.SetBool(AnimHash.WindowOn, isOpen);
        }

        protected override void Interaction()
        {
            if (!isOpen)
            {
                isOpen = !isOpen;
                anim.SetBool(AnimHash.WindowOn, isOpen);
            }
        }

        public void CloseWindow()
        {
            isOpen = !isOpen;
            anim.SetBool(AnimHash.WindowOn, isOpen);
        }
    }
}
