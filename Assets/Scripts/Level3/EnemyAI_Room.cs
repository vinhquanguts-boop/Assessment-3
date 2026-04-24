using UnityEngine;

public class EnemyAI_Room : MonoBehaviour {

    [Header("Stats")]
    [SerializeField] float health = 2f;
    [SerializeField] float speed = 2f;
    [SerializeField] float damageToPlayer = 10f;

    Room myRoom;
    Transform playerTransform;
    PlayerController playerScript;

    void Start() {
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null) {
            playerTransform = playerObj.transform;
            playerScript = playerObj.GetComponent<PlayerController>();
        }
    }

    public void SetRoom(Room room) {
        myRoom = room;
    }

    void Update() {
        if (playerTransform != null) {
            transform.position = Vector2.MoveTowards(
                transform.position,
                playerTransform.position,
                speed * Time.deltaTime
            );
        }
    }

    public void TakeDamage(float amount) {
        health -= amount;
        if (health <= 0) Die();
    }

    void Die() {
        if (myRoom != null) myRoom.OnEnemyDied();
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (playerScript != null) playerScript.TakeDamage(damageToPlayer);
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null) TakeDamage(bullet.damage);
            Destroy(other.gameObject);
        }
    }
}