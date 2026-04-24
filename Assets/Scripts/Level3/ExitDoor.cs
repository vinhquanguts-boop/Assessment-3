using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            LevelController controller = FindAnyObjectByType<LevelController>();
            if (controller != null) {
                controller.LevelComplete();
            } else {
                SceneManager.LoadScene("Level4");
            }
        }
    }
}