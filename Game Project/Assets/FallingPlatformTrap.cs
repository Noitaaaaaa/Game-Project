using System.Collections;
using UnityEngine;

public class PlatformTrap : MonoBehaviour
{
    [SerializeField] private float freezeDuration = 1f;
    private bool trapped = false;
    private Collider2D platformCollider;

    private void Start()
    {
        platformCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (trapped)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if player is above the platform (standing on top)
            if (IsPlayerOnTop(collision))
            {
                trapped = true;
                StartCoroutine(FreezePlayer(collision.gameObject));
            }
        }
    }

    private bool IsPlayerOnTop(Collision2D collision)
    {
        // Check if the collision normal points upward (player is on top)
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // If normal Y is negative, the platform pushed the player upward (player is on top)
            if (contact.normal.y < -0.5f)
                return true;
        }
        return false;
    }

    private IEnumerator FreezePlayer(GameObject player)
    {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero;
            playerRb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }

        yield return new WaitForSeconds(freezeDuration);

        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        trapped = false;
    }
}