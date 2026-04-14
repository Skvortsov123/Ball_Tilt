using System.Collections;
using UnityEngine;

public class playerSoundScript : MonoBehaviour
{
    [SerializeField] private AudioClip ballCollisionSFX, playerDeathSFX;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource rollingSource;
    private Rigidbody rb;
    private float playerVelocity;
    private float dynamicVolume;

    private bool isRespawning = false;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        playerVelocity = rb.angularVelocity.magnitude;
        dynamicVolume = Mathf.Clamp01(playerVelocity / 12f);
        rollingSource.volume = dynamicVolume;
    }

    private float GetDynamicVolume() // if a sound's strength depends on the speed of the collision like with a wall
    {
        return dynamicVolume;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (audioSource != null)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                audioSource.volume = GetDynamicVolume();
                audioSource.PlayOneShot(ballCollisionSFX);
            }

            //audioSource.volume = 1.0f;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (audioSource != null)
        {
            if (collision.gameObject.CompareTag("Hole Hazard") && !isRespawning)
            {
                audioSource.volume = 0.6f;
                audioSource.PlayOneShot(playerDeathSFX);
                isRespawning = true;
                StartCoroutine(RespawnCooldown());
            }

            //audioSource.volume = 1.0f;
        }
    }

    IEnumerator RespawnCooldown()
    {
        yield return new WaitForSeconds(1f);
        isRespawning = false;
    }
}
