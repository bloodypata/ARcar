using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
//using UnityEngine.Experimental.XR;


public class PlacementScript : MonoBehaviour
{
    private Pose placement;
    private ARRaycastManager aRRaycastManager;
    private bool placementIsValid = false;
    public GameObject indicator;
    public GameObject car;
    public bool carIsPlaced;

    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }


    void Update()
    {
        UpdatePlacement();
        UpdateIndicator();

        if (placementIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceCar();
        }
    }

    void PlaceCar()
    {

        if (carIsPlaced == false)
        {
            Instantiate(car, placement.position, placement.rotation);
            car.transform.Translate(Time.deltaTime * 1f, 0, 0);
            carIsPlaced = true;
            Destroy(indicator);

        }
        else if (carIsPlaced == true)
        {
            carIsPlaced = false;
           
        }
    }

    private void UpdateIndicator()
    {
        if (placementIsValid)
        {
            indicator.SetActive(true);
            indicator.transform.SetPositionAndRotation(placement.position, placement.rotation);
        }
        else
        {
            indicator.SetActive(false);
        }
    }

    void UpdatePlacement()
    {
        var screenCentre = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.55f));
        var hits = new List<ARRaycastHit>();

        aRRaycastManager.Raycast(screenCentre, hits, TrackableType.Planes);

        placementIsValid = hits.Count > 0;

        if (placementIsValid)
        {
            placement = hits[0].pose;
            var newRotation = Camera.current.transform.forward;
            var newBearing = new Vector3(newRotation.x,0,newRotation.z).normalized;
            placement.rotation = Quaternion.LookRotation(newBearing);
        }
    }

}
