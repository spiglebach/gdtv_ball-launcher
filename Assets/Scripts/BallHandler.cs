using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour {
    private Camera mainCamera;

    private void Awake() {
        mainCamera = Camera.main;
    }

    void Update() {
        var current = Touchscreen.current;
        if (current == null) return;
        var primaryTouch = current.primaryTouch;
        if (!primaryTouch.isInProgress) return;
        
        var touchPosition = primaryTouch.position.ReadValue();
        var worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
        
        Debug.Log(worldPosition.ToString());
    }
}
