using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] private RespawnScript respawn;

    private void Start()
    {
        if (respawn == null)
        {
            Debug.LogError("RespawnScript is not assigned in Inspector!", this);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (respawn != null)
            {
                respawn.Respawn();
            }
        }
    }
}