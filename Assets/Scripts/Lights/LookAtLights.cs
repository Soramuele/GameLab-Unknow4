using UnityEngine;

public class LookAtLights : MonoBehaviour
{
    [Header("Light LaryerMask")]
    [SerializeField] private LayerMask lightLayer;

    [SerializeField] private Transform eyes;

    private BrightnessManager brightnessManager;

    // Start is called the first frame before Update
    void Start()
    {
        brightnessManager = BrightnessManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(eyes.position, Camera.main.transform.forward * 5f, Color.red);
        if (Physics.Raycast(eyes.transform.position, Camera.main.transform.forward, out RaycastHit hit, 200f, lightLayer))
            brightnessManager.AddBrightness();
        else
            brightnessManager.SubtractBrightness();
    }
}
