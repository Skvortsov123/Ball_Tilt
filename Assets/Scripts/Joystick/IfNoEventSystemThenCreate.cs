using UnityEngine;
using UnityEngine.EventSystems;

public class IfNoEventSystemThenCreate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (FindFirstObjectByType<EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem");
            es.AddComponent<EventSystem>();
            es.AddComponent<StandaloneInputModule>(); // or InputSystemUIInputModule
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
