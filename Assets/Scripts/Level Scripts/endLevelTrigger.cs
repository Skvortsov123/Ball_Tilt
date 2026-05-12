using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class endLevelTrigger : MonoBehaviour
{
    [SerializeField] private String nextSceneIs;
    [SerializeField] private int currentLevel;
    [SerializeField] private int currentWorld;


   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeLevel();
        }
    }

    private void ChangeLevel()
    {
        int globalLevel = SaveManager.GetGlobalLevelIndex(currentWorld, currentLevel);

        SaveManager.completeLevel(globalLevel);
        SaveManager.checkWorldUnlock(currentWorld); // Kolla om nästa värld ska låsas upp
        SceneManager.LoadScene(nextSceneIs, LoadSceneMode.Single);
    }

}
