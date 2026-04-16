using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] float velocityToBreak;
    void OnCollisionEnter(Collision other)
    {
        GameObject colliderObject = other.gameObject;
        if (!colliderObject.CompareTag("Player")) return;

        if (HasEnoughVelocity(colliderObject))
        {
            //TODO: effekt för att vägen går sönder
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
