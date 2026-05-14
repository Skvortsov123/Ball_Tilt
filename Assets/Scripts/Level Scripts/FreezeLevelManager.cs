using UnityEngine;
using UnityEngine.SceneManagement;

public class FreezeLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject tapToStartPanel;

    private bool waitingForTap = true;

    void Start()
    {
        tapToStartPanel.SetActive(false);
        PauseGame();
    }

    void Update()
    {
        if (waitingForTap && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
        {
            StartGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        waitingForTap = true;
        tapToStartPanel.SetActive(true);
    }

    void StartGame()
    {
        Time.timeScale = 1f;
        waitingForTap = false;
        tapToStartPanel.SetActive(false);
    }

    public void RestartLevel()
    {
        // viktigt!
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("build index " + SceneManager.GetActiveScene().buildIndex );
    }
}