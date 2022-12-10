using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class OriginScript : MonoBehaviour
{
    public static string btnSelected;
    public GameObject mercuryPrefab;
    public GameObject venusPrefab;
    public GameObject earthPrefab;
    public GameObject marsPrefab;
    public GameObject jupiterPrefab;
    public GameObject saturnPrefab;
    public GameObject uranusPrefab;
    public GameObject neptunePrefab;
    public GameObject plutoPrefab; 
    GameObject landPrefab;
    Dictionary<GameObject, Tuple<GameObject, bool, bool>> prefabMap = new Dictionary<GameObject, Tuple<GameObject, bool, bool>>();
    GameObject landedObject;
    bool landed;

    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>(); // ar raycast hits a list of planes

    Vector2 touchPoint1;
    Vector2 touchPoint2;
    float currDistance;
    float prevDistance;
    bool firstPinch;
    GameObject prevPrefab;

    public static bool buttonClicked = false;
    //IEnumerator currTransCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (btnSelected == "Mercury")
        {
            landPrefab = mercuryPrefab;
        }
        else if (btnSelected == "Venus")
        {
            landPrefab = venusPrefab;
        }
        else if (btnSelected == "Earth")
        {
            landPrefab = earthPrefab;
        }
        else if (btnSelected == "Mars")
        {
            landPrefab = marsPrefab;
        }
        else if (btnSelected == "Jupiter")
        {
            landPrefab = jupiterPrefab;
        }
        else if (btnSelected == "Saturn")
        {
            landPrefab = saturnPrefab;
        }
        else if (btnSelected == "Uranus")
        {
            landPrefab = uranusPrefab;
        }
        else if (btnSelected == "Neptune")
        {
            landPrefab = neptunePrefab;
        }
        else if (btnSelected == "Pluto")
        {
            landPrefab = plutoPrefab;
        }

        transform(landPrefab);

        //if (Input.touchCount > 0)
        //{
        //    if (currTransCoroutine != null)
        //    {
        //        StopCoroutine(currTransCoroutine);
        //    }

        //    if (btnSelected == "Earth")
        //    {
        //        landPrefab = earthPrefab;
        //    }
        //    else if (btnSelected == "Mars")
        //    {
        //        landPrefab = marsPrefab;
        //    }
        //    currTransCoroutine = transform(landPrefab);
        //    StartCoroutine(currTransCoroutine);
        //}


    }

    void transform(GameObject landPrefab)
    {     
        if (!prefabMap.ContainsKey(landPrefab))
        {
            landed = false;
            firstPinch = true;
        }
        else
        {
            landedObject = prefabMap[landPrefab].Item1;
            landed = prefabMap[landPrefab].Item2;
            firstPinch = prefabMap[landPrefab].Item3;
            if (prevPrefab != null && prevPrefab != landPrefab)
            {
                firstPinch = true;
            }
        }

        if (Input.touchCount == 1) // touch point == 1. if user touch screen using one finger, land or move the object
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.tag == "Button" || hit.collider.tag == "Planet")
                {
                    if (hit.collider.name == "Earth")
                    {

                    }
                }
                else
                {
                    //yield return new WaitForSeconds(Time.deltaTime / 2f);
                                                                     
                    if (!buttonClicked && aRRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon)) // Input.GetTouch(0): first touch point. if user touch position within hits
                    {
                        var firstHitPose = hits[0].pose; // hit[0] is the first hitted plane
                        if (!landed) // if the object is not landed
                        {
                            landedObject = Instantiate(landPrefab, firstHitPose.position, firstHitPose.rotation);  // land the prefab at the plane's position and rotation
                            landed = true;
                            prefabMap.Add(landPrefab, new Tuple<GameObject, bool, bool>(landedObject, landed, firstPinch));
                        }
                        else
                        {
                            landedObject.transform.position = firstHitPose.position;
                        }                        
                    }                 
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
                prefabMap[landPrefab] = new Tuple<GameObject, bool, bool>(landedObject, landed, firstPinch);               
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
            if (prefabMap.ContainsKey(landPrefab))
            {
                prefabMap[landPrefab] = new Tuple<GameObject, bool, bool>(landedObject, landed, firstPinch);
            }
        }

        if (Input.touchCount == 3 && landed && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved && Input.GetTouch(2).phase == TouchPhase.Moved)  //rotate
        {
            landedObject.transform.Rotate(0f, Input.GetTouch(0).deltaPosition.x, 0f);
        }

        prevPrefab = landPrefab;
        buttonClicked = false;              
    }
}