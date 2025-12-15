using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections;
using UnityEngine.EventSystems;

public class ARPlacementController : MonoBehaviour
{
    [Header("AR")]
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    [Header("Placement")]
    public PlaceableObjectData selectedObject;


    [Header("UI")]
    public InstructionText instructionText;

    private GameObject placedObject;
    private static List<ARRaycastHit> hits = new();


    void Update()
    {
        Touch touch = Input.GetTouch(0);
        if (Input.touchCount > 0)
        {
            // Touch touch = Input.GetTouch(0);

            if (EventSystem.current != null &&
                EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return; // Touch is on UI, ignore AR interaction
            }
        }

        if (Input.touchCount == 0)
            return;

        if (placedObject != null)
            return;



        if (raycastManager == null)
        {
            return;
        }

        if (selectedObject == null || selectedObject.prefab == null)
        {
            return;
        }


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
            var interaction = placedObject.GetComponent<ARObjectInteraction>();
            if (interaction != null)
            {
                interaction.raycastManager = raycastManager;
            }
            instructionText.SetInstruction("Drag to move • Pinch to scale • Rotate with two fingers");
            DisablePlaneVisualization();
        }
    }

    public void SetSelectedObject(PlaceableObjectData data)
    {
        selectedObject = data;
        placedObject = null;
    }

    private void DisablePlaneVisualization()
    {
        planeManager.enabled = false;

        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }
}
