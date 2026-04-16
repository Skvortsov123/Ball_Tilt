using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class endLevelTrigger : MonoBehaviour
{
    [SerializeField] private String nextSceneIs;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeLevel();
        }
    }

    private void ChangeLevel()
    {
        SceneManager.LoadScene(nextSceneIs, LoadSceneMode.Single);
    }

}
