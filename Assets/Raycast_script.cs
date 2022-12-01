using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Raycast_script : MonoBehaviour
{
    public GameObject landPrefab;
    GameObject landedObject;
    bool landed;
    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>(); // ar raycast hits a list of planes


    // Start is called before the first frame update
    void Start()
    {
        landed = false;
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) // if user touch screen
        {   
            if (aRRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon)) // if user touch position within hits
            {
                var firstHitPose = hits[0].pose; // hit[0] is the first hitted plane
                if (!landed) // if the object is not landed
                {
                    landedObject = Instantiate(landPrefab, firstHitPose.position, firstHitPose.rotation);  // land the prefab at the plane's position and rotation
                    landed = true;
                }
                else
                {
                    landedObject.transform.position = firstHitPose.position;
                }
            }
            
        }
    }
}
