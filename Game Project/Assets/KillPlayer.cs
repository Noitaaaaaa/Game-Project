using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public GameObject Player;
    public Transform respawnPoint;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.transform.position = respawnPoint.position;
        }
    }
}


