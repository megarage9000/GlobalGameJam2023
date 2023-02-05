using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    private Slider slider;

    void Awake()
    {
       slider = gameObject.GetComponent<Slider>();
    }

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
