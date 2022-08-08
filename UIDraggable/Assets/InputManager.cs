using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public delegate void StartTouchEvent(Finger finger, float time);
    public event StartTouchEvent OnStartTouch;

    public delegate void EndTouchEvent(Finger finger, float time);
    public event EndTouchEvent OnEndTouch;

    public delegate void TouchMovedEvent(Finger finger, float time);
    public event TouchMovedEvent OnTouchMoved;

    public List<UIDragger> draggableUI { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            draggableUI = new List<UIDragger>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddDraggableUI(UIDragger obj)
    {
        if (draggableUI.Contains(obj)) return;

        draggableUI.Add(obj);
    }

    public void RemoveDraggableUI(UIDragger obj)
    {
        if (!draggableUI.Contains(obj)) return;

        draggableUI.Remove(obj);
    }

    public void EnableDraggableUI(UIDragger obj, bool canMove)
    {
        obj.EnableInput(canMove);
    }

    public void EnableDraggableUI(List<UIDragger> obj, bool canMove)
    {
        foreach(UIDragger uiObj in obj)
        {
            EnableDraggableUI(uiObj, canMove);
        }
    }

    private void OnEnable()
    {
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += FingerUp;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove += FingerMoved;
    }

    

    private void OnDisable()
    {
        TouchSimulation.Disable();
        EnhancedTouchSupport.Disable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp -= FingerUp;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove -= FingerMoved;
    }


    private void FingerDown(Finger finger)
    {
        if (OnStartTouch != null)
        {
            OnStartTouch(finger, Time.time);
        }
    }

    private void FingerUp(Finger finger)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch(finger, Time.time);
        }
    }

    private void FingerMoved(Finger finger)
    {
        if(OnTouchMoved != null)
        {
            OnTouchMoved(finger, Time.time);
        }
    }
}
