using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatButton : MonoBehaviour
{
    [SerializeField] Image m_image;
    [SerializeField] TextMeshProUGUI m_name;
    [SerializeField] ProgressBar m_progressBar;

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

        m_progressBar.SetAlwaysMax(stat.AlwaysMax);
        m_progressBar.ChangeSliderValue(stat.CurrentStat, stat.MaxStat);
        m_progressBar.SetProgressBarColour(statInfo.Colour);
    }     
}
