using UnityEngine;

public class DebugLevelDataReset : MonoBehaviour
{
    [SerializeField] private WorldSelector worldSelector;

    public void ResetData()
    {
        SaveManager.ResetAllData();

        worldSelector.RefreshCurrentWorld();

        Debug.Log("Save reset + UI refreshed");
    }
}