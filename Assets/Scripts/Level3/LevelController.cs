using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    [Header("Gates - assign in Inspector")]
    [SerializeField] Gate gateRoom1ToRoom2;
    [SerializeField] Gate gateRoom2ToRoom3_Left;
    [SerializeField] Gate gateRoom2ToRoom3_Right;
    [SerializeField] Gate gateRoom2ToRoom4;
    [SerializeField] Gate gateRoom3and4ToRoom5;
    [SerializeField] Gate gateRoom5ToRoom6;
    [SerializeField] Gate gateFinal;

    [Header("Scene Names")]
    [SerializeField] string currentScene = "Level3 2";
    [SerializeField] string nextScene = "Level4";

    bool room1Cleared = false;
    bool room2Cleared = false;
    bool room3Cleared = false;
    bool room4Cleared = false;
    bool room5Cleared = false;
    bool room6Cleared = false;
    bool keyCollected = false;
    bool levelComplete = false;

    // Win screen UI
    bool showWinScreen = false;
    float winScreenTimer = 0f;

    PlayerController player;

    void Start() {
        player = FindAnyObjectByType<PlayerController>();
        Debug.Log("=== LEVEL STARTED ===");
    }

    void Update() {
        if (player != null && player.health <= 0 && !levelComplete) {
            PlayerDied();
        }

        // Win screen countdown
        if (showWinScreen) {
            winScreenTimer -= Time.deltaTime;
            if (winScreenTimer <= 0f) {
                SceneManager.LoadScene(nextScene);
            }

            // Player can also press any key to continue
            if (Input.anyKeyDown) {
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    void OnGUI() {
        if (!showWinScreen) return;

        // Dark overlay
        GUI.color = new Color(0, 0, 0, 0.7f);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        GUI.color = Color.white;

        // Win text
        GUIStyle titleStyle = new GUIStyle();
        titleStyle.fontSize = 60;
        titleStyle.fontStyle = FontStyle.Bold;
        titleStyle.normal.textColor = Color.yellow;
        titleStyle.alignment = TextAnchor.MiddleCenter;

        GUIStyle subStyle = new GUIStyle();
        subStyle.fontSize = 28;
        subStyle.normal.textColor = Color.white;
        subStyle.alignment = TextAnchor.MiddleCenter;

        GUIStyle timerStyle = new GUIStyle();
        timerStyle.fontSize = 22;
        timerStyle.normal.textColor = new Color(0.8f, 0.8f, 0.8f);
        timerStyle.alignment = TextAnchor.MiddleCenter;

        float cx = Screen.width / 2f;
        float cy = Screen.height / 2f;

        GUI.Label(new Rect(cx - 300, cy - 120, 600, 100), "YOU ESCAPED!", titleStyle);
        GUI.Label(new Rect(cx - 300, cy - 20, 600, 60), "The Midway has been cleared.", subStyle);
        GUI.Label(new Rect(cx - 300, cy + 50, 600, 50), "Press any key to continue", timerStyle);
        GUI.Label(new Rect(cx - 300, cy + 100, 600, 50), "Continuing in " + Mathf.CeilToInt(winScreenTimer) + "s...", timerStyle);
    }

    public void RoomCleared(int roomID) {
        switch (roomID) {
            case 1:
                if (room1Cleared) return;
                room1Cleared = true;
                Debug.Log("Room 1 cleared!");
                OpenGate(gateRoom1ToRoom2);
                break;
            case 2:
                if (room2Cleared) return;
                room2Cleared = true;
                Debug.Log("Room 2 cleared!");
                OpenGate(gateRoom2ToRoom3_Left);
                OpenGate(gateRoom2ToRoom3_Right);
                OpenGate(gateRoom2ToRoom4);
                break;
            case 3:
                if (room3Cleared) return;
                room3Cleared = true;
                Debug.Log("Room 3 cleared!");
                CheckRoom3And4();
                break;
            case 4:
                if (room4Cleared) return;
                room4Cleared = true;
                Debug.Log("Room 4 cleared!");
                CheckRoom3And4();
                break;
            case 5:
                if (room5Cleared) return;
                room5Cleared = true;
                Debug.Log("Room 5 cleared!");
                OpenGate(gateRoom5ToRoom6);
                break;
            case 6:
                if (room6Cleared) return;
                room6Cleared = true;
                Debug.Log("Room 6 cleared! Find the key!");
                break;
        }
    }

    void CheckRoom3And4() {
        if (room3Cleared && room4Cleared) {
            Debug.Log("Room 3 and 4 cleared! Gate to Room 5 opening...");
            OpenGate(gateRoom3and4ToRoom5);
        } else {
            Debug.Log("Clear the other room too!");
        }
    }

    public void KeyCollected() {
        if (keyCollected) return;
        keyCollected = true;
        Debug.Log("Key collected! Final gate opening...");
        OpenGate(gateFinal);
    }

    public void LevelComplete() {
        if (levelComplete) return;
        levelComplete = true;
        showWinScreen = true;
        winScreenTimer = 5f;
        Time.timeScale = 0.0001f; // Pause game but keep GUI running
        Debug.Log("=== LEVEL COMPLETE! ===");
    }

    void OpenGate(Gate gate) {
        if (gate != null) gate.CheckAndOpen();
    }

    void PlayerDied() {
        Debug.Log("Player died! Restarting...");
        levelComplete = true;
        Time.timeScale = 1f;
        Invoke("RestartLevel", 1f);
    }

    void RestartLevel() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentScene);
    }
}