using UnityEngine;

public class FastMenu : MonoBehaviour
{
    private Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void toggleMenu()
    {
        animator.SetTrigger("Toggle");
    }

    public void toggleControl()
    {
        
    }

    public void calibrate()
    {
        
    }

    public void openSettings()
    {
        
        
    }

}
