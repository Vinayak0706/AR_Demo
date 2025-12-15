using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections;

public class ARPlacementController : MonoBehaviour
{
    [Header("AR")]
    public ARRaycastManager raycastManager;

    [Header("Placement")]
    public PlaceableObjectData selectedObject;

    private GameObject placedObject;
    private static List<ARRaycastHit> hits = new();


    void Update()
    {

        if (Input.touchCount == 0)
            return;

        if (placedObject != null)
            return;



        if (raycastManager == null)
        {
            Debug.LogWarning("ARRaycastManager reference is missing on ARPlacementController.");
            return;
        }

        if (selectedObject == null || selectedObject.prefab == null)
        {
            Debug.LogWarning("No placeable object selected or selected object's prefab is null.");
            return;
        }

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
            return;

        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;
            placedObject = Instantiate(
                selectedObject.prefab,
                pose.position,
                pose.rotation
            );

            placedObject.transform.localScale = selectedObject.defaultScale;
        }
    }

    public void SetSelectedObject(PlaceableObjectData data)
    {
        selectedObject = data;
        placedObject = null;
    }
}
