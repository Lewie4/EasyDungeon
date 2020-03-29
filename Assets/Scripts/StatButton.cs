using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatButton : MonoBehaviour
{
    [SerializeField] Image m_image;
    [SerializeField] TextMeshProUGUI m_name;
    [SerializeField] Image m_slider;
    [SerializeField] TextMeshProUGUI m_sliderText;

    bool m_alwaysMax;
    int m_max;

    public void Setup(StatButtonValues values, int currentValue, int maxValue)
    {
        m_image.sprite = values.statSprite;

        m_name.text = values.statName;

        m_alwaysMax = values.alwaysMax;
        ChangeSliderValue(currentValue, maxValue);

        m_slider.color = values.statColour;
    }

    public void ChangeSliderValue(int current, int max = -1)
    {
        if(max > 0)
        {
            m_max = max;
        }

        m_slider.fillAmount = m_alwaysMax ? 1 : (float)current/m_max;

        m_sliderText.text = m_alwaysMax ? m_max.ToString() : current + "/" + m_max;
    }
}
