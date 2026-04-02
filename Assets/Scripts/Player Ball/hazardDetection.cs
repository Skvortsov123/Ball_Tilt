using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public class hazardDetection : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hole Hazard"))
        {
            print("collision detected!");
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = spawnPosition.position;
        rb.linearVelocity = Vector3.zero;
    }
}
