using System.Collections;
using UnityEngine;


public class FallingBall : MonoBehaviour
{
    [SerializeField] private float topBoundary = 6f; // Y position above screen
    [SerializeField] private float leftBoundary = -8f; // Left edge
    [SerializeField] private float rightBoundary = 8f; // Right edge
    [SerializeField] private float teleportDelay = 5f; // Seconds

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(TeleportRoutine());
    }

    IEnumerator TeleportRoutine()
    {
        while (true) // Infinite loop for repeated teleporting
        {
            yield return new WaitForSeconds(teleportDelay);
            TeleportToTop();
        }
    }

    void TeleportToTop()
    {
        // 1. Generate random X position
        float randomX = Random.Range(leftBoundary, rightBoundary);

        // 2. Set new position
        transform.position = new Vector3(randomX, topBoundary, transform.position.z);

        teleportDelay = Random.Range(4f, 9f);

        rb.gravityScale = Random.Range(40f, 100f);

        // 3. Reset velocity so it doesn't keep falling fast immediately
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
