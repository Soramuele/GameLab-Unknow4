using UnityEngine;

namespace Unknown.Samuele
{
    public class RevolvingDoorSpin : MonoBehaviour
    {
        [SerializeField] private Transform door;
        [SerializeField, Range(0.1f, 10f)] private float spinSpeed = .5f;

        [Header("Entrance")]
        [SerializeField] private Transform[] entrances;
        
        // Update is called once per frame
        void Update()
        {
            door.Rotate(0, -spinSpeed, 0, Space.World);
        }

        public Transform GetEntrance() =>
            entrances[Random.Range(0, entrances.Length)];
    }
}
