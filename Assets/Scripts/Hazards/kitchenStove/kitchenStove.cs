using UnityEngine;
using System.Collections;

public class kitchenStove : MonoBehaviour
{
    private float currentTimer;

    [Header("Timer settings")]
    [SerializeField] private float timerDuration;
    [SerializeField] private float startDelay;

    [Header("Colors")]
    [SerializeField] private Color onColor = Color.red;
    [SerializeField] private Color offColor = Color.white;
    [SerializeField] private float transitionDuration = 1f;
    [Header("Particle System")]
    [SerializeField] private ParticleSystem stoveParticleSystem;
    [Header("Sound Sources")]
    [SerializeField] private AudioSource stoveAmbiance;

    [SerializeField] private AudioClip stoveAmbianceAudio;
    [SerializeField] private float reduceAudioMultiplier; //stove låter väldigt fel på max ljud
    private Renderer stoveRenderer;
    private bool isOn = false;

    void Start()
    {
        currentTimer = timerDuration;
        stoveRenderer = GetComponent<Renderer>();
        stoveRenderer.material.color = offColor;
        gameObject.tag = "stoveOff";

        stoveAmbiance.clip = stoveAmbianceAudio;
        stoveAmbiance.Play();
        stoveAmbiance.volume = GameSettings.sfxVolume * reduceAudioMultiplier;
    }

    void Update()
    {
        stoveAmbiance.volume = GameSettings.sfxVolume * reduceAudioMultiplier;
        if (startDelay > 0)
        {
            startDelay -= Time.deltaTime;
            return;
        }

        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            changeState();
            resetTimer();
        }
    }

    private void resetTimer()
    {
        currentTimer = timerDuration;
    }

    private void changeState()
    {
        isOn = !isOn;

      
        gameObject.tag = "stoveOff";

        StopAllCoroutines();

        if (isOn)
            StartCoroutine(LerpColor(onColor, setOnWhenDone: true));  
        else
            StartCoroutine(LerpColor(offColor, setOnWhenDone: false));
    }

    private IEnumerator LerpColor(Color targetColor, bool setOnWhenDone)
    {
        Color startColor = stoveRenderer.material.color;
        float timer = 0f;
        

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / transitionDuration);
            stoveRenderer.material.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        stoveRenderer.material.color = targetColor;

       
        if (setOnWhenDone){
            gameObject.tag = "stoveOn";
            ParticleSystem ps = Instantiate(stoveParticleSystem, transform.parent);
            ps.transform.localScale = transform.parent.localScale;
        }
    }
    public AudioSource GetAudioSource()
    {
        return stoveAmbiance;
    }
}