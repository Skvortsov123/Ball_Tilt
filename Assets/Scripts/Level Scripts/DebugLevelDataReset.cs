using UnityEngine;

public class DebugLevelDataReset : MonoBehaviour
{
    [SerializeField] private WorldSelector worldSelector;

    public void ResetLevelData()
    {
        SaveManager.ResetLevelData();

        worldSelector.RefreshCurrentWorld();

        Debug.Log("Save reset + UI refreshed");
    }
    public void ResetAllData()
    {
        SaveManager.ResetAllData();

        worldSelector.RefreshCurrentWorld();

        Debug.Log("Save reset + UI refreshed");
    }
}