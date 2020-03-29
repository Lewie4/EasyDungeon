using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI m_text;
    [SerializeField] protected Image m_slider;

    public virtual void OnClick()
    {

    }

    protected void SetFill(float amount)
    {
        m_slider.fillAmount = amount;
    }
}
