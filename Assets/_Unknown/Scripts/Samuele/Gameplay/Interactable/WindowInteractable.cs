using UnityEngine;

namespace Unknown.Samuele
{
    public class WindowInteractable : Interactable
    {
        [Header("Data")]
        [SerializeField] private Animator anim;
        public bool isOpen;

        // Start is called before the first frame update
        void Awake()
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
