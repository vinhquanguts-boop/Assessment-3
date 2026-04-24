using UnityEngine;

public class Room : MonoBehaviour {

    [Header("Settings")]
    public int roomID;

    [Header("Enemies to spawn when player enters")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] int enemyCount = 0;

    [Header("Key to spawn when cleared (Room 6 only)")]
    [SerializeField] GameObject keyPrefab;
    [SerializeField] Transform keySpawnPoint;

    [HideInInspector] public bool isCleared = false;

    bool playerEntered = false;
    int remainingEnemies = 0;
    LevelController levelController;

    void Start() {
        levelController = FindAnyObjectByType<LevelController>();

        // Check Is Trigger warning
        Collider2D col = GetComponent<Collider2D>();
        if (col != null && !col.isTrigger)
            Debug.LogWarning("Room " + roomID + ": Box Collider 2D Is Trigger is OFF! Enemies will not spawn. Please enable Is Trigger.");
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !playerEntered && !isCleared) {
            playerEntered = true;
            Debug.Log("Player entered Room " + roomID + " - spawning enemies!");
            SpawnEnemies();
        }
    }

    void SpawnEnemies() {
        if (enemyCount == 0) {
            ClearRoom();
            return;
        }

        if (enemyPrefab == null) {
            Debug.LogWarning("Room " + roomID + ": No enemy prefab assigned!");
            ClearRoom();
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0) {
            Debug.LogWarning("Room " + roomID + ": No spawn points assigned!");
            ClearRoom();
            return;
        }

        remainingEnemies = Mathf.Min(enemyCount, spawnPoints.Length);

        for (int i = 0; i < remainingEnemies; i++) {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity);
            EnemyAI_Room ai = enemy.GetComponent<EnemyAI_Room>();
            if (ai != null) ai.SetRoom(this);
        }

        Debug.Log("Room " + roomID + " spawned " + remainingEnemies + " enemies!");
    }

    public void OnEnemyDied() {
        remainingEnemies--;
        Debug.Log("Room " + roomID + " enemies remaining: " + remainingEnemies);
        if (remainingEnemies <= 0 && !isCleared) ClearRoom();
    }

    void ClearRoom() {
        isCleared = true;
        Debug.Log("Room " + roomID + " cleared!");

        if (keyPrefab != null && keySpawnPoint != null)
            Instantiate(keyPrefab, keySpawnPoint.position, Quaternion.identity);

        if (levelController != null) levelController.RoomCleared(roomID);
    }
}