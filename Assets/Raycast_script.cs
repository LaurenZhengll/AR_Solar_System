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


    Vector2 touchPoint1;
    Vector2 touchPoint2;
    float currDistance;
    float prevDistance;
    bool firstPinch;
    
    // Start is called before the first frame update
    void Start()
    {
        landed = false;
        aRRaycastManager = GetComponent<ARRaycastManager>();
        firstPinch = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1) // touch point == 1. if user touch screen using one finger, land or move the object
        {
            if (aRRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon)) // Input.GetTouch(0): first touch point. if user touch position within hits
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

        if (Input.touchCount == 2 && landed)  //if user touch screen using two fingers, scale the object
        {
            touchPoint1 = Input.GetTouch(0).position;
            touchPoint2 = Input.GetTouch(1).position;
            currDistance = touchPoint2.magnitude - touchPoint1.magnitude;
            if (firstPinch)
            {
                prevDistance = currDistance;
                firstPinch = false;
            }
            if (currDistance != prevDistance)
            {
                landedObject.transform.localScale *= currDistance / prevDistance;
                prevDistance = currDistance;
            }
        }
        else
        {
            firstPinch = true;
        }

        if (Input.touchCount == 3 && landed && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved && Input.GetTouch(2).phase == TouchPhase.Moved)  //rotate
        {
            landedObject.transform.Rotate(0f, Input.GetTouch(0).deltaPosition.x, 0f);
        }

    }
}
