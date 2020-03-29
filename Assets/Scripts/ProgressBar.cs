using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Image m_slider;
    [SerializeField] TextMeshProUGUI m_sliderText;

    public void ChangeSliderValue(int current, int max)
    {
        m_slider.fillAmount = (float)current/max;

        m_sliderText.text = current + "/" + max;
    }
}
