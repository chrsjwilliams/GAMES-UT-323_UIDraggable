using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class Draggable3DManager : MonoBehaviour
{
    // To cache references for better perfromance
    private Camera mainCamera;
    private InputManager inputManager;

    [SerializeField] private bool disableInput;

    private float dist;
    private Vector3 offset;
    private IDraggable3D toDrag;

    Vector3 pos;

    private void Awake()
    {
        inputManager = InputManager.Instance;
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        // Subscribe to the OnStartTouchEvent
        inputManager.OnStartTouch += StartTouch;
        inputManager.OnEndTouch += EndTouch;
        inputManager.OnTouchMoved += TouchMoved;
    }

    private void OnDisable()
    {
        // Subscribe to the OnStartTouchEvent
        inputManager.OnStartTouch += StartTouch;
        inputManager.OnEndTouch += EndTouch;
        inputManager.OnTouchMoved += TouchMoved;
    }

    public void DiableInput(bool b)
    {
        disableInput = b;
        if (toDrag != null)
        {
            EndTouch(toDrag.id, Time.time);
        }
    }

    private void StartTouch(Finger finger, float time)
    {
        if (disableInput) return;

        pos = new Vector3();
        Ray ray = mainCamera.ScreenPointToRay(finger.screenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Check to see if our raycast hit an object of our super class type
            var hitObject = hit.collider.gameObject.GetComponent<SomeParentObjectClass>();
            // only continue if the hit object implements the IDraggable3D interface
            if (hitObject is IDraggable3D && ((IDraggable3D)hitObject).canMove)
            {
                toDrag = (IDraggable3D)hitObject;

                toDrag.id = finger;
                // adjust dist so our objects remain on the same depth plane
                dist = hit.transform.position.z - mainCamera.transform.position.z;

                pos = new Vector3(finger.screenPosition.x, finger.screenPosition.y, dist);
                pos = mainCamera.ScreenToWorldPoint(pos);

                // calculate offset
                offset = toDrag.pos - pos;
            }
        }
    }

    private void TouchMoved(Finger finger, float time)
    {
        if (toDrag == null || disableInput) return;
        if (!toDrag.canMove) return;

        pos = new Vector3(finger.screenPosition.x, finger.screenPosition.y, dist);
        pos = mainCamera.ScreenToWorldPoint(pos);
        toDrag.pos = pos + offset;
    }

    private void EndTouch(Finger finger, float time)
    {
        if (toDrag == null || toDrag.id != finger) return;

        toDrag.id = null;
        toDrag = null;
    }
}
