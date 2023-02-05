using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    public Slider slider;

    
    public void SetValue(int value)
    {
        slider.value = value;
    }

    public void SetMaxValue(int value)
    {
        slider.maxValue = value;
    }

    public void SetMinValue(int value)
    {
        slider.minValue = value;
    }
}
