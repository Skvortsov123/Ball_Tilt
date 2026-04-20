using UnityEngine;
using UnityEngine.UI;

public class FastMenu : MonoBehaviour
{
    private Animator animator;

    [Header("Control Mode")]
    public GameObject joystick;
    public GameObject sliderUI;

    // public TiltControl tiltControl;
    public TiltControl[] tiltControls; // NYTT: stöd för flera bollar

    public Button calibrateButton;
    public Slider sensitivitySlider;
    public Slider deadzoneSlider;
    public Slider musicSlider;
    public Slider volumeSlider;

    [Header("Menus")]
    public GameObject settingsPanel;
    public GameObject fastMenu;

    [Header("Control Icons")]
    public Image controlButtonImage;
    public Sprite tiltIcon;
    public Sprite joystickIcon;
    public Sprite sliderIcon;

    public void Start()
    {
        animator = GetComponent<Animator>();

        // tiltControl = GameObject.FindGameObjectWithTag("Player")
        //                 .GetComponent<TiltControl>();

        tiltControls = FindObjectsByType<TiltControl>(FindObjectsSortMode.None); // NYTT: hämta alla bollar
        Debug.Log("TiltControls found: " + tiltControls.Length);

        UpdateControlUI(); // uppdatera UI baserat på mode

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
        switch (GameSettings.controlMode) // rotera mellan 3 lägen
        {
            case ControlMode.Tilt:
                GameSettings.controlMode = ControlMode.Joystick;
                break;

            case ControlMode.Joystick:
                GameSettings.controlMode = ControlMode.Slider;
                break;

            case ControlMode.Slider:
                GameSettings.controlMode = ControlMode.Tilt;
                break;
        }

        UpdateControlUI(); // uppdatera UI efter byte
    }

    void UpdateControlUI()
    {
        joystick.SetActive(GameSettings.controlMode == ControlMode.Joystick);
        sliderUI.SetActive(GameSettings.controlMode == ControlMode.Slider);

        calibrateButton.interactable = (GameSettings.controlMode == ControlMode.Tilt);

        // byt ikon beroende på läge
        switch (GameSettings.controlMode)
        {
            case ControlMode.Tilt:
                controlButtonImage.sprite = tiltIcon;
                break;

            case ControlMode.Joystick:
                controlButtonImage.sprite = joystickIcon;
                break;

            case ControlMode.Slider:
                controlButtonImage.sprite = sliderIcon;
                break;
        }
    }

    public void Calibrate()
    {
        // var tiltControl = GameObject.FindGameObjectWithTag("Player")
        //                            .GetComponent<TiltControl>();

        foreach (var tc in tiltControls) // NYTT: kalibrera alla
        {
            tc.Calibrate();
            Debug.Log("Calibration triggered on: " + tc.name);
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
        // tiltControl.SetSensitivity(value);

        foreach (var tc in tiltControls) // NYTT: ändra alla
        {
            tc.SetSensitivity(value);
        }
    }

    public void SetDeadZone(float value)
    {
        // tiltControl.SetDeadZone(value);

        foreach (var tc in tiltControls) // NYTT: ändra alla
        {
            tc.SetDeadZone(value);
        }
    }
}