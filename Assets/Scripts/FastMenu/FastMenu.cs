using UnityEngine;
using UnityEngine.UI;

public class FastMenu : MonoBehaviour
{
    private Animator animator;

    [Header("Control Mode")]
    public GameObject joystick;
    public TiltControl tiltControl;
    public Button calibrateButton;
    public Slider sensitivitySlider;
    public Slider deadzoneSlider;
    public Slider musicSlider;
    public Slider volumeSlider;



    private bool joystickActive = false;

    [Header("Menus")]
    public GameObject settingsPanel;
    public GameObject fastMenu;



    public void Start()
    {
        animator = GetComponent<Animator>();

        tiltControl = GameObject.FindGameObjectWithTag("Player")
                        .GetComponent<TiltControl>();
        Debug.Log("TiltControl found: " + tiltControl); 

        joystickActive = GameSettings.useJoystick;

        joystick.SetActive(joystickActive);

        tiltControl.useTilt = !joystickActive;
        tiltControl.useJoystick = joystickActive;

        calibrateButton.interactable = !joystickActive;

        settingsPanel.SetActive(false);

        sensitivitySlider.value = GameSettings.sensitivity;
        deadzoneSlider.value = GameSettings.deadZone;
        musicSlider.value = GameSettings.musicVolume;
        volumeSlider.value = GameSettings.sfxVolume;
    }


    public void toggleMenu()
    {
        animator.SetTrigger("Toggle");
    }

    public void toggleControl()
    {
        // Toggle the state
        joystickActive = !joystickActive;

        GameSettings.useJoystick = joystickActive;

        // Show/hide joystick
        joystick.SetActive(joystickActive);

        // Switch input modes
        tiltControl.useTilt = !joystickActive;
        tiltControl.useJoystick = joystickActive;

        // Enable/disable calibrate button
        calibrateButton.interactable = !joystickActive;

        Debug.Log("Joystick Active: " + joystickActive);
    }

    public void Calibrate()
    {
        var tiltControl = GameObject.FindGameObjectWithTag("Player")
                                   .GetComponent<TiltControl>();

        if (tiltControl != null)
        {
            tiltControl.Calibrate();
            Debug.Log("Calibration triggered on: " + tiltControl.name);
        }
        else
        {
            Debug.LogWarning("No TiltControl found!");
        }
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

    public void SetSensitivity(float value)
    {
        
        tiltControl.SetSensitivity(value);
    }

    public void SetDeadZone(float value)
    {
        tiltControl.SetDeadZone(value);
    }

}
