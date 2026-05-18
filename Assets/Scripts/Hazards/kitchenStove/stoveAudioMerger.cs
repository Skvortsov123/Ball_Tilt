using UnityEngine;

public class stoveAudioMerger : MonoBehaviour
{
    kitchenStove[] stoves;
   [SerializeField] AudioClip audioClip;
   [SerializeField] AudioSource audioMaster;
   [SerializeField] private float AudioMultiplier; //stove låter väldigt fel på max ljud
   
    void Start()
    {
        stoves  = FindObjectsByType<kitchenStove>(FindObjectsSortMode.None);
        mergeStovesAudioSource();
        audioMaster.volume = GameSettings.sfxVolume * AudioMultiplier;
    }
    void Update()
    {
        audioMaster.volume = GameSettings.sfxVolume * AudioMultiplier;
    }

    private void mergeStovesAudioSource()
    {
        if(stoves.Length < 2) return;
        foreach(kitchenStove s in stoves){
           if(s.Equals(this)) continue;
            AudioSource audioSource =  s.GetComponentInChildren<kitchenStove>().GetAudioSource();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.mute = true;
            audioSource.Stop();
            Debug.Log("Turned of " + s.ToString() +" audioSource");
        }
        audioMaster.clip = audioClip;
        audioMaster.Play();
    }
  
}
