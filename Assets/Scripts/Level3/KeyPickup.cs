using UnityEngine;

public class KeyPickup : MonoBehaviour {

    [SerializeField] Gate gateToUnlock;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            LevelController controller = FindAnyObjectByType<LevelController>();
            if (controller != null) controller.KeyCollected();
            if (gateToUnlock != null) gateToUnlock.OpenWithKey();
            Debug.Log("Key collected!");
            Destroy(gameObject);
        }
    }
}