using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] GameObject clownPrefab; // Drag your Orange Square prefab here
    [SerializeField] float spawnRate = 2f;    // How many seconds between spawns
    [SerializeField] float spawnDistance = 10f; // How far away from player they appear

    Transform player;
    float nextSpawnTime;

    void Start() {
        player = GameObject.Find("Player").transform;
    }

    void Update() {
        if (Time.time >= nextSpawnTime) {
            SpawnClown();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnClown() {
        if (player == null) return;

        // Pick a random direction and multiply by distance
        Vector2 randomDir = Random.insideUnitCircle.normalized * spawnDistance;
        Vector3 spawnPos = player.position + new Vector3(randomDir.x, randomDir.y, 0);

        Instantiate(clownPrefab, spawnPos, Quaternion.identity);
    }
}