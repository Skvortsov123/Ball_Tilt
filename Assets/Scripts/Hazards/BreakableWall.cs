using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] float velocityToBreak;
    [SerializeField] AudioClip breakSound;  //Dra in vilket ljud som ska spelas när väggen går sönder beroende på vad för vägg det är glas/sten
    void OnCollisionEnter(Collision other)
    {
        GameObject colliderObject = other.gameObject;
        if (!colliderObject.CompareTag("Player")) return;

        if (HasEnoughVelocity(colliderObject))
        {
            //TODO: effekt för att vägen går sönder
            AudioManager.Instance.PlaySFX(breakSound, 0.4f); //Spela upp ljudet när väggen går sönder
            Destroy(gameObject);
        }
    }
    private bool HasEnoughVelocity(GameObject o)
    {
        Vector3 lastVelocity = o.GetComponent<TiltControl>().getLastVelocity();
        float deltaV = (o.GetComponent<Rigidbody>().linearVelocity - lastVelocity).magnitude;
        return deltaV > velocityToBreak;

    }
}
