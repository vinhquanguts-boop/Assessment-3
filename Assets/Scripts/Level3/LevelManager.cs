using UnityEngine;

public class LevelManager : MonoBehaviour {

    void Start() {
        Debug.Log("Level started. Clear all rooms to escape!");
    }

    public void OnRoomCleared(int roomID) {
        Debug.Log("Room " + roomID + " reported cleared to LevelManager.");
    }
}
