using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour {
    [SerializeField] private Rigidbody2D currentBallRigidbody;
    private Camera mainCamera;

    private void Awake() {
        mainCamera = Camera.main;
    }

    void Update() {
        var current = Touchscreen.current;
        if (current == null) {
            MakeBallDynamic();
            return;
        }
        var primaryTouch = current.primaryTouch;
        if (!primaryTouch.isInProgress) {
            MakeBallDynamic();
            return;
        }
        
        var touchPosition = primaryTouch.position.ReadValue();
        var worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        currentBallRigidbody.position = worldPosition;
        currentBallRigidbody.isKinematic = true;
    }

    private void MakeBallDynamic() {
        currentBallRigidbody.isKinematic = false;
    }
}
