using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void SetMaxHelp(int help)
    {
        slider.maxValue = help;
        slider.value = 0;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHelp(int help)
    {
        slider.value = help;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
