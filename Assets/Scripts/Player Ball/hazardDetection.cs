using System.Collections;
using UnityEngine;

public class hazardDetection : MonoBehaviour
{
    [SerializeField] private TransitionManager transition;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private string[] hazardTags = { "Hole Hazard" };

    private Rigidbody rb;

    private bool isRespawning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isRespawning) return;
        foreach (string tag in hazardTags)
        {
            if (other.CompareTag(tag))
            {
                TriggerRespawnAll(); // gemensam funktion
                break;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (isRespawning) return;
        foreach (string tag in hazardTags)
        {
            if (other.CompareTag(tag))
            {
                TriggerRespawnAll();
                break;
            }
        }
    }

    //hantera kollision mellan bollar
    private void OnCollisionEnter(Collision other)
    {
        if (isRespawning) return;

        // kolla om vi tr�ffar en annan boll (har samma script)
        if (other.gameObject.GetComponent<hazardDetection>() != null)
        {
            TriggerRespawnAll(); //  samma death som hazard
        }
    }

    void TriggerRespawnAll()
    {
        isRespawning = true;

        foreach (var ball in FindObjectsByType<hazardDetection>(FindObjectsSortMode.None))
        {
            ball.StartCoroutine(ball.Respawn());
        }
    }



    IEnumerator Respawn()
    {
        transition.ActivateHazardHoleDeathAnimation();

        //rb.isKinematic = true;


        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;


        // Hitta slider och resetta input
        var slider = FindFirstObjectByType<SliderControl>();
        if (slider != null)
        {
            slider.ResetToCenter();
        }


        yield return new WaitForSeconds(1.2f);

        rb.MovePosition(spawnPosition.position);

        yield return new WaitForFixedUpdate();

        //rb.isKinematic = false;

        isRespawning = false;
    }
}
