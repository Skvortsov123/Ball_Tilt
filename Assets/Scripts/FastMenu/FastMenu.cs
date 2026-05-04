using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FastMenu : MonoBehaviour
{
    private Animator animator;

    [Header("Control Mode")]
    public GameObject joystickLeft;
    public GameObject joystickRight;
    public GameObject joystickTouch;
    public GameObject sliderUI;
    public GameObject joystickModeButton;


    public TiltControl[] tiltControls; // st�d f�r flera bollar

    public Button calibrateButton;
    public Slider sensitivitySlider;
    public Slider deadzoneSlider;

    [Header("Sound")]
    public AudioClip clickSound;
    public Slider musicSlider;
    public Slider volumeSlider;
    public Toggle toggleMuted;

    [Header("Menus")]
    public GameObject settingsPanel;
    public GameObject fastMenu;


    [Header("Control Icons")]
    public Image controlButtonImage;
    public Image joystickButtonImage;
    public Sprite tiltIcon;
    public Sprite joystickIcon;
    public Sprite sliderIcon;
    public Sprite leftIcon;
    public Sprite rightIcon;
    public Sprite touchIcon;

    void Start()
    {
        animator = GetComponent<Animator>();



        // H�mta alla bollar i scenen
        tiltControls = FindObjectsByType<TiltControl>(FindObjectsSortMode.None);
        Debug.Log("TiltControls found: " + tiltControls.Length);

        UpdateControlUI();

        settingsPanel.SetActive(false);

        // --- S�TT INITIALA V�RDEN P� SLIDERS (viktigt att g�ra f�re listeners) ---
        sensitivitySlider.value = GameSettings.sensitivity;
        deadzoneSlider.value = GameSettings.deadZone;
        musicSlider.value = GameSettings.musicVolume;
        volumeSlider.value = GameSettings.sfxVolume;

        toggleMuted.isOn = GameSettings.musicMuted;

        // --- KOPPLA SLIDERS TILL AUDIO MANAGER (via singleton) ---
        // Tar bort gamla listeners först för att undvika duplicates om UI laddas flera gånger
        var audio = AudioManager.Instance;

        if (audio != null)
        {
            musicSlider.onValueChanged.RemoveAllListeners();
            volumeSlider.onValueChanged.RemoveAllListeners();
            toggleMuted.onValueChanged.RemoveAllListeners();

            musicSlider.onValueChanged.AddListener(audio.SetMusicVolume);
            volumeSlider.onValueChanged.AddListener(audio.SetSFXVolume);
            toggleMuted.onValueChanged.AddListener(AudioManager.Instance.ToggleMusic);
        }
        else
        {
            Debug.LogWarning("AudioManager.Instance is null");
        }

        // --- KOPPLA GAMEPLAY SLIDERS ---
        sensitivitySlider.onValueChanged.RemoveAllListeners();
        deadzoneSlider.onValueChanged.RemoveAllListeners();

        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
        deadzoneSlider.onValueChanged.AddListener(SetDeadZone);
    }

    public void toggleMenu()
    {
        animator.SetTrigger("Toggle");
        AudioManager.Instance.PlaySFX(clickSound);
    }

    public void toggleControl()
    {
        // V�xla mellan control modes
        switch (GameSettings.controlMode)
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
        AudioManager.Instance.PlaySFX(clickSound);
        UpdateControlUI();
    }

    public void toggleJoystick()
    {
        switch (GameSettings.joystickMode)
        {
            case JoystickMode.Left:
                GameSettings.joystickMode = JoystickMode.Right;
                break;

            case JoystickMode.Right:
                GameSettings.joystickMode = JoystickMode.Touch;
                break;
            
            case JoystickMode.Touch:
                GameSettings.joystickMode = JoystickMode.Left;
                break;
        }
        AudioManager.Instance.PlaySFX(clickSound);
        UpdateControlUI();
    }

    void UpdateControlUI()
    {
        joystickModeButton.SetActive(GameSettings.controlMode == ControlMode.Joystick);
        if (GameSettings.controlMode == ControlMode.Joystick)
        {
            joystickLeft.SetActive(GameSettings.joystickMode == JoystickMode.Left);
            joystickRight.SetActive(GameSettings.joystickMode == JoystickMode.Right);
            joystickTouch.SetActive(GameSettings.joystickMode == JoystickMode.Touch);
        }
        else
        {
            joystickLeft.SetActive(false);
            joystickRight.SetActive(false);
            joystickTouch.SetActive(false);
        }


        sliderUI.SetActive(GameSettings.controlMode == ControlMode.Slider);

        calibrateButton.interactable = (GameSettings.controlMode == ControlMode.Tilt);

        // Byt ikon beroende p� control mode
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

        // Change icon on joystick mode button
        switch (GameSettings.joystickMode)
        {
            case JoystickMode.Left:
                joystickButtonImage.sprite = leftIcon;
                break;

            case JoystickMode.Right:
                joystickButtonImage.sprite = rightIcon;
                break;

            case JoystickMode.Touch:
                joystickButtonImage.sprite = touchIcon;
                break;
        }
    }

    public void Calibrate()
    {
        AudioManager.Instance.PlaySFX(clickSound);
        GameSettings.calibrationOffset = Input.gyro.gravity;
    }

    public void OpenSettings()
    {
        AudioManager.Instance.PlaySFX(clickSound);
        settingsPanel.SetActive(true);

        if (fastMenu != null)
            fastMenu.SetActive(false);
    }

    public void CloseSettings()
    {
        AudioManager.Instance.PlaySFX(clickSound);
        settingsPanel.SetActive(false);

        if (fastMenu != null)
            fastMenu.SetActive(true);
    }

    public void SetSensitivity(float value)
    {
        // Uppdatera alla bollar
        foreach (var tc in tiltControls)
        {
            tc.SetSensitivity(value);
        }
    }

    public void SetDeadZone(float value)
    {
        GameSettings.deadZone = value;
    }



}