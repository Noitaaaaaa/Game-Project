using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] private RespawnScript respawn;
    private BoxCollider2D checkPointCollider;

    void Awake()
    {
        checkPointCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (respawn != null)
            {
                respawn.SetRespawnPoint(transform.position); // ✅ use public setter
            }

            checkPointCollider.enabled = false;
        }
    }
}