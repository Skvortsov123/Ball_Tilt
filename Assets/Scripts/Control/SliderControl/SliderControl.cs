using UnityEngine;
using UnityEngine.UI;


public class SliderControl : MonoBehaviour
{
    float x;
    float y;
    public Slider xSlider;
    public Slider ySlider;

    public void OnEnable()
    {
        var tiltControls = FindObjectsByType<TiltControl>(FindObjectsSortMode.None);

        foreach (var tc in tiltControls)
        {
            tc.SetSliderControl(this);
        }
    }

    public void ResetToCenter()
    {
        // Sätt input till mitten (ingen rörelse)
        x = 0f;
        y = 0f;
        if (xSlider != null) xSlider.value = 0f;
        if (ySlider != null) ySlider.value = 0f;
    }

    public void SetX(float x)
    {
        this.x = x;
    }

    public void SetY(float y)
    {
        this.y = y;
    }

    public float GetX()
    {
        return x;
    }

    public float GetY()
    {
        return y;
    }
}
