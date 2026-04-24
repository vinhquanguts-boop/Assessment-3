using UnityEngine;

public class EnemyAI : MonoBehaviour {
    [Header("Enemy Stats")]
    [SerializeField] float health = 2f;
    [SerializeField] float speed = 2f;
    [SerializeField] float damageToPlayer = 10f;
    
    Transform playerTransform;
    PlayerController playerScript;

    void Start() {
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null) {
            playerTransform = playerObj.transform;
            playerScript = playerObj.GetComponent<PlayerController>();
        }
    }

    void Update() {
        if (playerTransform != null) {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }

    // This is called when a bullet hits the clown
    public void TakeDamage(float amount) {
        health -= amount;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            playerScript.TakeDamage(damageToPlayer);
            Destroy(gameObject); 
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            // Get the bullet's damage value (we'll set this up next)
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null) {
                TakeDamage(bullet.damage);
            }
            Destroy(other.gameObject); // Destroy the bullet
        }
    }
}