using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip currentTrack;


    private void Awake()
    {
        // --- NY KOD ---
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // undvik duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // överlever scenbyten


        // Skapa AudioSources om de inte finns
        if (musicSource == null)
            musicSource = gameObject.AddComponent<AudioSource>();

        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;

    }


    //kör innan någon scen laddas, så att ljudet är redo direkt
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        GameObject obj = new GameObject("AudioManager");
        obj.AddComponent<AudioManager>();
        
    }


    private void Start()
    {
        musicSource.volume = GameSettings.musicVolume;
        sfxSource.volume = GameSettings.sfxVolume;
        musicSource.mute = GameSettings.musicMuted;

        AudioClip clip = Resources.Load<AudioClip>("Audio/My Song 13");
        PlayMusic(clip);

    }

    //för att spela ny låt vid ny värld exempelvis
    public void PlayMusic(AudioClip newClip)
    {
        // Om samma låt redan spelas -> gör inget
        if (currentTrack == newClip)
            return;

        currentTrack = newClip;

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();
    }

    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
        GameSettings.musicVolume = value;
    }

    public void SetSFXVolume(float value)
    {
        sfxSource.volume = value;
        GameSettings.sfxVolume = value;
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        GameSettings.musicMuted = musicSource.mute;
    }
}