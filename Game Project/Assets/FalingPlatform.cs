using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float destroyDelay = 2f;

    private bool falling = false;
    private Rigidbody2D rb;
    private Collider2D platformCollider;

    private void Start()
    {
        // Get components automatically
        rb = GetComponent<Rigidbody2D>();
        platformCollider = GetComponent<Collider2D>();

        // Validate setup
        if (rb == null)
        {
            Debug.LogError("FallingPlatform: Rigidbody2D not found! Add a Rigidbody2D component.");
            return;
        }

        if (platformCollider == null)
        {
            Debug.LogError("FallingPlatform: Collider2D not found! Add a Collider2D component.");
            return;
        }

        // Ensure it starts as kinematic
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 1f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Avoid calling the coroutine multiple times
        if (falling)
            return;

        // Check if the player landed on the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected! Platform will fall in " + fallDelay + " seconds.");
            StartCoroutine(StartFall());
        }
    }

    private IEnumerator StartFall()
    {
        falling = true;

        // Wait before dropping
        yield return new WaitForSeconds(fallDelay);

        // Enable gravity and make it dynamic
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;

        // Destroy after falling
        Destroy(gameObject, destroyDelay);
    }
}