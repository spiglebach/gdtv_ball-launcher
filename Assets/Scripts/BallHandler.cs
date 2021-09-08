using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour {
    [SerializeField] private Rigidbody2D currentBallRigidbody;
    [SerializeField] private SpringJoint2D currentBallSpringJoint;
    [SerializeField][Range(.05f, 1f)] private float detachDelayInSeconds = 0.1f;
    private Camera mainCamera;
    private bool isDragging;

    private void Awake() {
        mainCamera = Camera.main;
    }

    void Update() {
        if (!currentBallRigidbody) return;
        var current = Touchscreen.current;
        if (current == null) {
            return;
        }
        var primaryTouch = current.primaryTouch;
        if (!primaryTouch.isInProgress) {
            if (isDragging) {
                LaunchBall();
            }
            isDragging = false;
            return;
        }

        isDragging = true;
        
        var touchPosition = primaryTouch.position.ReadValue();
        var worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        currentBallRigidbody.position = worldPosition;
        currentBallRigidbody.isKinematic = true;
    }

    private void LaunchBall() {
        currentBallRigidbody.isKinematic = false;
        currentBallRigidbody = null;
        Invoke(nameof(DetachBall), detachDelayInSeconds);
    }

    private void DetachBall() {
        currentBallSpringJoint.enabled = false;
        currentBallSpringJoint = null;
    }
}
