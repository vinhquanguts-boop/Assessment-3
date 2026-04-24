using UnityEngine;

public class Gate : MonoBehaviour {

    [HideInInspector] public bool isOpen = false;

    SpriteRenderer spriteRenderer;
    Collider2D gateCollider;

    void Start() {
        // Search in children too in case sprite is on child object
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        gateCollider = GetComponent<Collider2D>();
        SetClosed();
    }

    public void CheckAndOpen() { Open(); }
    public void OpenWithKey() { Open(); }

    void Open() {
        if (isOpen) return;
        isOpen = true;

        // Disable collider so player can pass through
        if (gateCollider != null) gateCollider.enabled = false;

        // Hide the gate visual
        if (spriteRenderer != null) spriteRenderer.enabled = false;

        // Also hide any child renderers
        SpriteRenderer[] allRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in allRenderers) sr.enabled = false;

        Debug.Log(gameObject.name + " opened!");
    }

    void SetClosed() {
        if (spriteRenderer != null) spriteRenderer.color = Color.red;
        SpriteRenderer[] allRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in allRenderers) sr.color = Color.red;
    }
}