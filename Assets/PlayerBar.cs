using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    public Slider slider;
    public void SetMaxHelp(int help)
    {
        slider.maxValue = help;
        slider.value = help;
    }

    public void SetHelp(int help)
    {

    }
}
