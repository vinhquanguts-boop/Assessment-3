using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] float speed = 5f;
    [SerializeField] float bulletSpeed = 12f;
    [SerializeField] float fireRate = 0.25f;
    [SerializeField] GameObject bulletPrefab;

    [Header("Stats")]
    public float health = 100f;

    float nextFireTime;
    Camera mainCam;
    Vector2 shootDir;
    Vector2 moveInput;
    Rigidbody2D rb;

    void Start() {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W)) moveY = 1f;
        if (Input.GetKey(KeyCode.S)) moveY = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        moveInput = new Vector2(moveX, moveY).normalized;

        UpdateShootDirection();

        if (shootDir != Vector2.zero && Time.time >= nextFireTime) {
            Shoot(shootDir);
            nextFireTime = Time.time + fireRate;
        }

        if (health <= 0) Destroy(gameObject);
    }

    void FixedUpdate() {
        if (rb != null) rb.linearVelocity = moveInput * speed;
    }

    void UpdateShootDirection() {
        shootDir = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow)) shootDir = Vector2.up;
        else if (Input.GetKey(KeyCode.DownArrow)) shootDir = Vector2.down;
        else if (Input.GetKey(KeyCode.LeftArrow)) shootDir = Vector2.left;
        else if (Input.GetKey(KeyCode.RightArrow)) shootDir = Vector2.right;
        else if (Input.GetMouseButton(0)) {
            Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 directionToMouse = (mousePos - transform.position).normalized;

            if (Mathf.Abs(directionToMouse.x) > Mathf.Abs(directionToMouse.y)) {
                shootDir = (directionToMouse.x > 0) ? Vector2.right : Vector2.left;
            } else {
                shootDir = (directionToMouse.y > 0) ? Vector2.up : Vector2.down;
            }
        }

        if (shootDir != Vector2.zero) {
            float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }

    void Shoot(Vector2 dir) {
        if (bulletPrefab == null) return;
        GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = b.GetComponent<Rigidbody2D>();
        if (bulletRb != null) bulletRb.linearVelocity = dir * bulletSpeed;
        Destroy(b, 2f);
    }

    public void TakeDamage(float amount) { health -= amount; }
}