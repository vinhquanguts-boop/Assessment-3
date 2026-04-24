using UnityEngine;

public class HUD : MonoBehaviour {

    PlayerController player;

    void Start() {
        player = FindAnyObjectByType<PlayerController>();
    }

    void OnGUI() {
        if (player == null) return;

        GUI.Box(new Rect(10, 10, 160, 35), "");

        GUIStyle style = new GUIStyle();
        style.fontSize = 22;
        style.normal.textColor = Color.white;
        style.fontStyle = FontStyle.Bold;

        GUI.Label(new Rect(20, 15, 150, 35), "Health: " + Mathf.CeilToInt(player.health), style);
    }
}