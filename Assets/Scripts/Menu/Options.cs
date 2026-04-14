using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Options : MonoBehaviour
{
    
    public UnityEngine.UI.Slider sensitivitySlider;



    void Start()
    {
        
        sensitivitySlider.onValueChanged.AddListener(delegate { sensitivitySliderCheck(); });

    }

    public void sensitivitySliderCheck()
    {

        Debug.Log("Sensitivity: " + GetComponent<UnityEngine.UI.Slider>().value);
        PlayerPrefs.SetFloat("Sensitivity", GetComponent<UnityEngine.UI.Slider>().value);
    }


}
