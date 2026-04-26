using UnityEngine;

public class SandSlowdown : MonoBehaviour
{
    [SerializeField] private int slownessValue;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().linearDamping = slownessValue;
            
        }
    }
    private void OnTriggerExit(Collider other) // resetta dampening
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().linearDamping = 0.5f;

        }
    }

}
