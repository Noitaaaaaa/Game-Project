using UnityEngine;
using System.Collections;

public class FreezePlayer : MonoBehaviour
{
    [SerializeField] private float freezeDuration = 2f;
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered || !collision.gameObject.CompareTag("Player"))
            return;

        triggered = true;
        StartCoroutine(Freeze(collision.gameObject));
    }

    private IEnumerator Freeze(GameObject player)
    {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
            // Stop horizontal movement only
            playerRb.linearVelocity = new Vector2(0, playerRb.linearVelocity.y);
            // Freeze only X position and rotation
            playerRb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }

        // Wait for freeze duration
        yield return new WaitForSeconds(freezeDuration);

        // Restore normal constraints
        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        triggered = false;
    }
}