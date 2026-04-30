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

    //avgör om detta är huvudspelaren (fullt ljud + death-ljud)
    public bool isMainPlayer = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        playerVelocity = rb.angularVelocity.magnitude;
        dynamicVolume = Mathf.Clamp01(playerVelocity / 12f);

        //enemy får lägre volym
        float volumeMultiplier = isMainPlayer ? 2f : 0.6f;

        rollingSource.volume = dynamicVolume * GameSettings.sfxVolume * volumeMultiplier;
    }

    private float GetDynamicVolume()
    {
        return dynamicVolume;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (audioSource != null)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                // enemy får lägre volym
                float volumeMultiplier = isMainPlayer ? 1f : 0.6f;

                audioSource.volume = GetDynamicVolume() * GameSettings.sfxVolume * volumeMultiplier;
                audioSource.PlayOneShot(ballCollisionSFX);
            }

            //bollkollision = deathljud
            if (collision.gameObject.GetComponent<hazardDetection>() != null && !isRespawning)
            {
                // endast huvudspelaren spelar death-ljud
                if (isMainPlayer)
                {
                    audioSource.volume = 0.6f * GameSettings.sfxVolume;
                    audioSource.PlayOneShot(playerDeathSFX);
                }

                isRespawning = true;
                StartCoroutine(RespawnCooldown());
            }


        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (audioSource != null)
        {
            if (collision.gameObject.CompareTag("Hole Hazard") && !isRespawning)
            {


                audioSource.volume = 0.6f * GameSettings.sfxVolume;
                audioSource.PlayOneShot(playerDeathSFX);

                isRespawning = true;
                StartCoroutine(RespawnCooldown());
            }
        }
    }

    IEnumerator RespawnCooldown()
    {
        yield return new WaitForSeconds(1f);
        isRespawning = false;
    }
}