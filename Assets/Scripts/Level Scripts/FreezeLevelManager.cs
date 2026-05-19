using UnityEngine;
using UnityEngine.SceneManagement;

public class FreezeLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject tapToStartPanel;

    private bool waitingForTap = true;

    void Awake()
    {
        SaveManager.loadSettings();
    }

    void Start()
    {
        tapToStartPanel.SetActive(false);
        PauseGame();
    }

    void Update()
    {
        if (!waitingForTap) return;

        // Editor / PC test
        if (Input.GetMouseButtonDown(0))
        {
            StartGame();
            return;
        }

        // Mobil-touch (bara när ny touch börjar)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            StartGame();
        }
    }

    void PauseGame()
    {
        AudioManager.Instance.toggleSFX(true);
        Time.timeScale = 0f;
        waitingForTap = true;
        tapToStartPanel.SetActive(true);
    }

    void StartGame()
    {
        AudioManager.Instance.toggleSFX(false);
        Time.timeScale = 1f;
        waitingForTap = false;
        tapToStartPanel.SetActive(false);
        SaveManager.loadSettings();
    }

    public void RestartLevel()
    {
        // viktigt!
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("build index " + SceneManager.GetActiveScene().buildIndex );
    }
}