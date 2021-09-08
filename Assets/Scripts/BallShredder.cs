using UnityEngine;

public class BallShredder : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ball")) {
            Destroy(other.gameObject);
        }
    }
}
