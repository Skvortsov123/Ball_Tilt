using UnityEngine;
using UnityEngine.UI;

public class FastMenu : MonoBehaviour
{
    private Animator animator;

    [Header("Control Mode")]
    public GameObject joystick;
    public TiltControl tiltControl;
    public Button calibrateButton;

    private bool joystickActive = false;

    [Header("Menus")]
    public GameObject settingsPanel;
    public GameObject fastMenu;

    public void Start()
    {
        animator = GetComponent<Animator>();
        joystick.SetActive(false);
        tiltControl.enabled = true;
        calibrateButton.interactable = true;
    }

    public void toggleMenu()
    {
        animator.SetTrigger("Toggle");
    }

    public void toggleControl()
    {
        joystickActive = !joystickActive;

        // Toggle joystick
        joystick.SetActive(joystickActive);

        // Toggle tilt control (opposite)
        tiltControl.enabled = !joystickActive;

        // Enable/disable calibrate button
        calibrateButton.interactable = !joystickActive;


    }

    public void calibrate()
    {
        
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);

        if (fastMenu != null)
            fastMenu.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);

        if (fastMenu != null)
            fastMenu.SetActive(true);
    }

}
