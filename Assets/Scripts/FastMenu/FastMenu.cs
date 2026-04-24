using UnityEngine;
using UnityEngine.UI;

public class FastMenu : MonoBehaviour
{
    private Animator animator;

    [Header("Control Mode")]
    public GameObject joystick;
    public GameObject sliderUI;

    public TiltControl[] tiltControls; // stöd för flera bollar

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

    void Start()
    {
        animator = GetComponent<Animator>();



        // Hämta alla bollar i scenen
        tiltControls = FindObjectsByType<TiltControl>(FindObjectsSortMode.None);
        Debug.Log("TiltControls found: " + tiltControls.Length);

        UpdateControlUI();

        settingsPanel.SetActive(false);

        // --- SÄTT INITIALA VÄRDEN PÅ SLIDERS (viktigt att göra före listeners) ---
        sensitivitySlider.value = GameSettings.sensitivity;
        deadzoneSlider.value = GameSettings.deadZone;
        musicSlider.value = GameSettings.musicVolume;
        volumeSlider.value = GameSettings.sfxVolume;

        // --- KOPPLA SLIDERS TILL AUDIO MANAGER (via singleton) ---
        // Tar bort gamla listeners först för att undvika duplicates om UI laddas flera gånger
        var audio = AudioManager.Instance;

        if (audio != null)
        {
            musicSlider.onValueChanged.RemoveAllListeners();
            volumeSlider.onValueChanged.RemoveAllListeners();

            musicSlider.onValueChanged.AddListener(audio.SetMusicVolume);
            volumeSlider.onValueChanged.AddListener(audio.SetSFXVolume);
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
    }

    public void toggleControl()
    {
        // Växla mellan control modes
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

        UpdateControlUI();
    }

    void UpdateControlUI()
    {
        joystick.SetActive(GameSettings.controlMode == ControlMode.Joystick);
        sliderUI.SetActive(GameSettings.controlMode == ControlMode.Slider);

        calibrateButton.interactable = (GameSettings.controlMode == ControlMode.Tilt);

        // Byt ikon beroende på control mode
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
        // Kalibrera alla bollar
        foreach (var tc in tiltControls)
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
        // Uppdatera alla bollar
        foreach (var tc in tiltControls)
        {
            tc.SetSensitivity(value);
        }
    }

    public void SetDeadZone(float value)
    {
        // Uppdatera alla bollar
        foreach (var tc in tiltControls)
        {
            tc.SetDeadZone(value);
        }
    }
}