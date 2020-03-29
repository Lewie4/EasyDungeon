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
    float m_max;

    public void Setup(StatInfo statInfo, Stat stat)
    {
        if (statInfo.Icon != null)
        {
            m_image.sprite = statInfo.Icon;
        }
        else
        {
            m_image.color = statInfo.Colour;
        }

        m_name.text = statInfo.ShortName;

        m_alwaysMax = stat.AlwaysMax;
        ChangeSliderValue(stat.CurrentStat, stat.MaxStat);

        m_slider.color = statInfo.Colour;
    }

    public void ChangeSliderValue(float current, float max = -1)
    {
        if(max > 0)
        {
            m_max = max;
        }

        m_slider.fillAmount = m_alwaysMax ? 1 : current/m_max;

        m_sliderText.text = m_alwaysMax ? ((int)m_max).ToString() : (int)current + "/" + (int)m_max;
    }
}
