using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player;

    [Header("Respawn")]
    private Vector2 respawnPoint;

    void Start()
    {
        // Default spawn = starting position
        respawnPoint = player.transform.position;
    }

    public void SetRespawnPoint(Vector2 newPoint)
    {
        respawnPoint = newPoint;
    }

    public void Respawn()
    {
        player.transform.position = respawnPoint;
    }
}