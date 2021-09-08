using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour {
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Rigidbody2D pivot;
    [SerializeField][Range(.05f, 1f)] private float detachDelayInSeconds = 0.1f;
    [SerializeField] private float respawnDelay = 1f;
    
    private Camera mainCamera;
    private bool isDragging;
    private Rigidbody2D currentBallRigidbody;
    private SpringJoint2D currentBallSpringJoint;

    private void Awake() {
        mainCamera = Camera.main;
    }

    private void Start() {
        SpawnNewBall();
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
        Invoke(nameof(SpawnNewBall), respawnDelay);
    }

    private void DetachBall() {
        currentBallSpringJoint.enabled = false;
        currentBallSpringJoint = null;
    }

    private void SpawnNewBall() {
        var ballInstance = Instantiate(ballPrefab, pivot.position, Quaternion.identity);
        currentBallRigidbody = ballInstance.GetComponent<Rigidbody2D>();
        currentBallSpringJoint = ballInstance.GetComponent<SpringJoint2D>();

        currentBallSpringJoint.connectedBody = pivot;
    }
}
