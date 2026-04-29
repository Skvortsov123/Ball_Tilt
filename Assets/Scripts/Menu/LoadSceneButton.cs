using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] string sceneName; // Namnet på scenen som ska laddas
    [SerializeField] AudioClip clickSound; // Ljud som spelas när knappen trycks

    public void LoadScene()
    {
        AudioManager.Instance.PlaySFX(clickSound);

        // Laddar scenen med namnet som du skrivit in i Inspector
        SceneManager.LoadScene(sceneName);
    }
}