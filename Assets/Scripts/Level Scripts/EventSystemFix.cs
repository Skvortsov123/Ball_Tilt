using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemFix : MonoBehaviour
{
    static bool exists = false;

    void Awake()
    {
        if (exists)
        {
            Destroy(gameObject);
            return;
        }

        exists = true;
        DontDestroyOnLoad(gameObject);
    }
}