using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] float velocityToBreak;
    void OnCollisionEnter(Collision other)
    {
        GameObject colliderObject = other.gameObject;
        if (!playerCheck(colliderObject))
        {
            return;
        }
        if (enoughVelocity(colliderObject))
        {
            //TODO: effekt för att vägen går sönder
            Destroy(gameObject);
        }
    }

    private bool playerCheck(GameObject o)
    {
        if (o.CompareTag("Player")) return true;
        return false;
    }

    private bool enoughVelocity(GameObject o)
    {

        Vector3 lastVelocity = o.GetComponent<TiltControl>().getLastVelocity();
        float deltaV = (o.GetComponent<Rigidbody>().linearVelocity - lastVelocity).magnitude;
        if (deltaV > velocityToBreak)
        {
            return true;
        }
        return false;

    }
}
