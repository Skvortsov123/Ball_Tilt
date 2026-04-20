using UnityEngine;

public class catAttackTrigger : MonoBehaviour
{
    [SerializeField] private GameObject pawPrefab;
    [SerializeField] private float timer = 1f;
    private float currentTimer;
    private float delayTimer = 0.2f;
    private bool runDelayTimer = false;

    private void Start()
    {
        currentTimer = timer;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        runDelayTimer = false;
        delayTimer = 0.2f;
    }


    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        runDelayTimer = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            attack();
        }
    }
    private void attack()
    {
        currentTimer = timer;


        Instantiate(pawPrefab, transform.position, transform.rotation);
    }

    private void Update()
    {
        if (!runDelayTimer) return;

        delayTimer -= Time.deltaTime;
        if (delayTimer <= 0)
        {
            delayTimer = 0.2f;
            currentTimer = timer;
            runDelayTimer = false;
        }
    }
}
