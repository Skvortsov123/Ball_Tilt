using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelMenu;
    [SerializeField] AudioClip clickSound;
    public Animator animator;

    void Start()
    {
        // När spelet startar visas mainMenu och levelMenu göms
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void PlayPressed()
    {
        // När spelaren trycker på Play:
        //kanske nån animation och ljud här??
        AudioManager.Instance.PlaySFX(clickSound);
        animator.SetTrigger("pressPlay");

    }

    public void toLevelSelector()
    {
        // Dölj mainMenu
        // sen Visa levelMenu
        //efter en animationevent i PlayStartAnimation
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

}