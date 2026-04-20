using UnityEngine;

public class SetGravity : MonoBehaviour
{
    public float strength = -20f;
    void Start()
    {
        Physics.gravity = new Vector3(0, strength, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
