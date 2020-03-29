using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Image m_slider;
    [SerializeField] TextMeshProUGUI m_sliderText;
    [SerializeField] int m_numberOfDecimals;

    bool m_alwaysMax;

    public void ChangeSliderValue(float current, float max)
    {
        m_slider.fillAmount = m_alwaysMax ? 1 : current/max;

        if (m_numberOfDecimals >= 0)
        {
            m_sliderText.text = m_alwaysMax ? Math.Round(max, m_numberOfDecimals).ToString() : Math.Round(current, m_numberOfDecimals) + "/" + Math.Round(max, m_numberOfDecimals);
        }
        else
        {
            m_sliderText.text = m_alwaysMax ? max.ToString() : current + "/" + max;
        }
    }

    public void SetAlwaysMax(bool isAlwaysMax)
    {
        m_alwaysMax = isAlwaysMax;
    }

    public void SetProgressBarColour(Color barColour)
    {
        m_slider.color = barColour;
    }
}
