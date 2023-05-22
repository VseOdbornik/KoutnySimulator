using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    Slider slider;

    void Start()
    {
        slider= GetComponent<Slider>();
    }

    public void SetValue(float value)
    {
        slider.value = value;
    }

    public void SetMaxValue(float maxValue)
    {
        slider.maxValue = maxValue;
    }
}
