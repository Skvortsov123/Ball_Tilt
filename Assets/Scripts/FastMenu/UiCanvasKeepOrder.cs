using UnityEngine;

public class UiCanvasKeepOrder : MonoBehaviour
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

        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.overrideSorting = true;
            canvas.sortingOrder = 10;
        }
    }
}