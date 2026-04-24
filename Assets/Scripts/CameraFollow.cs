using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] Transform target; // Drag your Player here
    [SerializeField] Vector3 offset = new Vector3(0, 0, -10); // Keeps camera at a distance

    void LateUpdate() {
        if (target != null) {
            // Follow the player's position + the Z offset
            transform.position = target.position + offset;
        }
    }
}