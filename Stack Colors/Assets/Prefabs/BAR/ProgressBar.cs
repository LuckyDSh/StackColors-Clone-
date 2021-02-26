/*
* TickLuck Team
* All rights reserved
*/

using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    #region Variables 
    [HideInInspector] public static Slider s_meltBarSlider;
    public static float maxTime = 100f; 
    public Gradient gradient;
    public Image fill;

    [HideInInspector]
    public static bool isOn;
    #endregion

    #region UnityMethods
    private void Start()
    {
        maxTime = 0;
        
        s_meltBarSlider = gameObject.GetComponent<Slider>();
        s_meltBarSlider.value = maxTime;
        isOn = true;
       
    }

    private void Update()
    {
        if (isOn)
        {
            maxTime = PlayerStackColorController.forwardForce;
            SetProgress(maxTime);
        }
    }
    #endregion

    public void SetMaxValue(int maxvalue)
    {
        s_meltBarSlider.maxValue = maxvalue;
        s_meltBarSlider.value = maxvalue;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetProgress(float value)
    {
        s_meltBarSlider.value = value;

        fill.color = gradient.Evaluate(s_meltBarSlider.normalizedValue);//
    }
}
