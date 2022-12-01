using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Estimate_light : MonoBehaviour
{
    public ARCameraManager aRCamera;
    Light light;

    void OnEnable()
    {
        aRCamera.frameReceived += getLight;
    }

    void OnDisable()
    {
        aRCamera.frameReceived -= getLight;
    }

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void getLight(ARCameraFrameEventArgs args)
    {
        Debug.Log(args.lightEstimation);
    }
}
