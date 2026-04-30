using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
   [SerializeField] private float timeAlive;
    void Start()
    {
    Invoke("destroyThis",timeAlive);        
    }
    private void destroyThis()
    {
        Destroy(gameObject);
    }
    
}
