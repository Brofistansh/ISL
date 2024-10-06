using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;

public class ARObjectPlacer : MonoBehaviour
{
    public GameObject objectToPlace; // Assign your AR object in the Inspector
    private ARRaycastManager raycastManager;
    private GameObject spawnedObject;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(touch.position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

                if (hits.Count > 0)
                {
                    var hitPose = hits[0].pose;

                    if (spawnedObject == null)
                    {
                        spawnedObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
                    }
                    else
                    {
                        spawnedObject.transform.position = hitPose.position;
                    }
                }
            }
        }
    }
}
