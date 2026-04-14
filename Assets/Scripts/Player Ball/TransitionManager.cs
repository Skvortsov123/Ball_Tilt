using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    void Start()
    {
        
    }

    public void ActivateHazardHoleDeathAnimation()
    {
        animator.SetTrigger("Start");
    }
}
