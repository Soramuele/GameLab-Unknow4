using UnityEngine;

namespace Unknown.Samuele
{
    public class RevolvingDoorSpin : MonoBehaviour
    {
        [SerializeField] private Transform door;
        [SerializeField, Range(0.1f, 10f)] private float spinSpeed = .5f;
        
        // Update is called once per frame
        void Update()
        {
            door.Rotate(0, -spinSpeed, 0, Space.World);
        }
    }
}
