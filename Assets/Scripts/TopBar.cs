using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TopBar : MonoBehaviour
{
    public enum UI
    {
        None,
        HardCurrency,
        SoftCurrency,
        Level,
        SoftKey,
        HardKey
    }

    [System.Serializable]
    public class ImageAndText
    {
        public Image image;
        public TextMeshProUGUI text;
    }

    [SerializeField] ImageAndText m_hardCurrency;
    [SerializeField] ImageAndText m_softCurrency;
    [SerializeField] ImageAndText m_level;
    [SerializeField] ImageAndText m_softKey;
    [SerializeField] ImageAndText m_hardKey;

    public void UpdateUI(TopBar.UI ui, string text, Sprite sprite = null)
    {
        switch (ui)
        {
            case UI.HardCurrency:
                {
                    UpdateImageAndText(m_hardCurrency, text, sprite);
                    break;
                }
            case UI.SoftCurrency:
                {
                    UpdateImageAndText(m_softCurrency, text, sprite);
                    break;
                }
            case UI.Level:
                {
                    UpdateImageAndText(m_level, text, sprite);
                    break;
                }
            case UI.SoftKey:
                {
                    UpdateImageAndText(m_softKey, text, sprite);
                    break;
                }
            case UI.HardKey:
                {
                    UpdateImageAndText(m_hardKey, text, sprite);
                    break;
                }
        }
    }

    private void UpdateImageAndText(ImageAndText imageAndText, string text, Sprite sprite = null)
    {
        if(sprite != null)
        {
            imageAndText.image.sprite = sprite;
        }

        imageAndText.text.text = text;
    }
}
