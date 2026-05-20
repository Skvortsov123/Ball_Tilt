using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Hanterar world selection-menyn
public class WorldSelector : MonoBehaviour
{
    [SerializeField] GameObject[] worlds; // Alla world panels
    [SerializeField] AudioClip clickSound;

    private int currentWorld = 0;

    [SerializeField] private Button[] worldButtons;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private TextMeshProUGUI completionText;

    void Start()
    {
        ShowWorld(currentWorld);
    }

    // Byter till nästa world
    public void NextWorld()
    {
        currentWorld++;

        // Börja om från början om vi passerar sista
        if (currentWorld >= worlds.Length)
            currentWorld = 0;

        ShowWorld(currentWorld);

        AudioManager.Instance.PlaySFX(clickSound);
    }

    // Byter till föregående world
    public void PreviousWorld()
    {
        currentWorld--;

        // Hoppa till sista världen om vi går under 0
        if (currentWorld < 0)
            currentWorld = worlds.Length - 1;

        ShowWorld(currentWorld);

        AudioManager.Instance.PlaySFX(clickSound);
    }

    // Visar vald world och uppdaterar UI
    void ShowWorld(int index)
    {
        // Visa bara vald world
        for (int i = 0; i < worlds.Length; i++)
        {
            worlds[i].SetActive(i == index);
        }

        int worldIndex = index + 1;

        // Kollar om världen är upplåst
        bool unlocked = SaveManager.IsWorldUnlocked(worldIndex);

        // Aktivera/inaktivera knapp
        worldButtons[index].interactable = unlocked;

        // Visa låsikon om världen är låst
        lockImage.SetActive(!unlocked);

        // Visa completion % för världen
        int percent = SaveManager.getWorldCompletionPercentage(worldIndex);

        completionText.text = percent + "%";
    }

    // Uppdaterar världen som visas just nu
    public void RefreshCurrentWorld()
    {
        ShowWorld(currentWorld);
    }

    public void DebugUnlockAll()
    {
        SaveManager.UnlockAllLevels();

        RefreshCurrentWorld();
    }
}