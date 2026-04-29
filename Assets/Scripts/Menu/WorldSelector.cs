using UnityEngine;

public class WorldSelector : MonoBehaviour
{
    [SerializeField] GameObject[] worlds; // Array med alla world panels
    [SerializeField] AudioClip clickSound; // Ljud som spelas
    private int currentWorld = 0; 

    void Start()
    {
        // Visa första världen när scenen startar
        ShowWorld(currentWorld);
    }

    public void NextWorld()
    {
        // Gå till nästa värld
        currentWorld++;

        // Om vi går förbi sista världen börja om från början
        if (currentWorld >= worlds.Length)
            currentWorld = 0;

        // Uppdatera vilken värld som visas
        ShowWorld(currentWorld);

        AudioManager.Instance.PlaySFX(clickSound);

    }

    public void PreviousWorld()
    {
        // Gå till förra världen
        currentWorld--;

        // Om vi går under 0 hoppa till sista världen
        if (currentWorld < 0)
            currentWorld = worlds.Length - 1;

        // Uppdatera vilken värld som visas
        ShowWorld(currentWorld);

        AudioManager.Instance.PlaySFX(clickSound);
    }

    void ShowWorld(int index)
    {
        // Loopa igenom alla worlds
        for (int i = 0; i < worlds.Length; i++)
        {
            // Aktivera endast den world som matchar index
            worlds[i].SetActive(i == index);
        }
    }
}