using UnityEngine;

public class FanScript : MonoBehaviour
{

    public float force = 10f;
    public Vector3 direction = Vector3.up; // Direction the fan blows
    public bool useLocalDirection = true;  // Use fan's rotation

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        if (rb != null && other.CompareTag("Player"))
        {
            Vector3 dir = useLocalDirection ? transform.TransformDirection(direction) : direction;

            rb.AddForce(dir * force, ForceMode.Force);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 1, 0.2f);

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            Gizmos.matrix = transform.localToWorldMatrix;

            if (col is BoxCollider box)
            {
                Gizmos.DrawCube(box.center, box.size);
            }
        }

        

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
