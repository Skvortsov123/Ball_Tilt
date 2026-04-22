using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderReset : MonoBehaviour, IPointerUpHandler
{
    private Slider slider;

    public void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        slider.value = 0f;
    }
}