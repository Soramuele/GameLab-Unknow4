using UnityEngine;

namespace Unknown.Samuele
{
    public class Parallax : Pausable
    {
        [Header("Speed")]
        [SerializeField] private float parallaxSpeed = 1f;

        private Material material;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            
            material = GetComponent<MeshRenderer>().material;
        }

        // Update is called once per frame
        void Update()
        {
            material.mainTextureOffset += new Vector2(parallaxSpeed * Time.deltaTime, 0);
        }
    }
}
