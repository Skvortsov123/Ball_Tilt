using UnityEngine;

public class catAttack : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        
    }
}