using UnityEngine;

public class WallCollisionParticles : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem crashParticles; 
    public Rigidbody rb;

    [Header("Settings")]
    public float minImpactSpeed = 1f;

    void OnCollisionEnter(Collision other)
    {
        // Only trigger on objects tagged "Wall"
        if (!other.gameObject.CompareTag("Wall"))
        {
            
            return;
        }

        // ignore tiny bumps
        if (rb.linearVelocity.magnitude < minImpactSpeed)
        {
            
            return;
        }
            

        ContactPoint contact = other.contacts[0];

        // Move particles to collision point
        crashParticles.transform.position = contact.point;

        // Face particles away from wall
        crashParticles.transform.rotation = Quaternion.LookRotation(contact.normal);

        // Try to match particle color to wall material color
        Renderer wallRenderer = other.gameObject.GetComponent<Renderer>();

        if (wallRenderer != null)
        {
            Material wallMat = wallRenderer.material;
            //Debug.Log("Material found: " + wallMat.name);

            if (wallMat.HasProperty("_Color"))
            {
                //Debug.Log("Material has _Color property");

                Color wallColor = wallMat.color;

                //Debug.Log("Wall color detected: " + wallColor);

                var main = crashParticles.main;
                main.startColor = new ParticleSystem.MinMaxGradient(wallColor);
                //Debug.Log("Particle color changed to: " + wallColor);
            }
            else
            {
                //Debug.LogWarning("Material does NOT have _Color property");
            }
        }
        else
        {
            //Debug.LogWarning("No Renderer found on object: " + other.gameObject.name);
        }

        // Play crash effect
        crashParticles.Play();

    }
}