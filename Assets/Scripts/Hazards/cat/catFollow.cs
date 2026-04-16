using UnityEngine;

public class catFollow : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool canMove;
    private GameObject player;



    private void Start()
    {
        findPlayerGameObject();

    }
    private void Update()
    {
        followPlayer();
    }
    private void findPlayerGameObject()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void followPlayer()
    {
        if (!canMove) return;
        Vector3 targetPos = player.transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);

    }
    public void changeCanMove(bool newValue)
    {
        canMove = newValue;
    }
}