using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatButtonValues
{
    public Sprite statSprite;
    public string statName;
    public Color statColour;
    public bool alwaysMax;
}

public class StatButtons : MonoBehaviour
{
    [System.Serializable]
    public class StatButtonSetup
    {
        public StatButton statButton;
        public StatButtonValues statButtonValues;
    }

    [SerializeField] StatButtonSetup m_healthButton;
    [SerializeField] StatButtonSetup m_attackButton;
    [SerializeField] StatButtonSetup m_defenceButton;
    [SerializeField] StatButtonSetup m_energyButton;
    [SerializeField] StatButtonSetup m_recoveryButton;

    public void Start()
    {
        SetupButton(StatEnum.Health, 100, 100);
        SetupButton(StatEnum.Attack, 50, 50);
        SetupButton(StatEnum.Defence, 10, 10);
        SetupButton(StatEnum.Energy, 500, 500);
        SetupButton(StatEnum.Recovery, 75, 75);
    }

    public void SetupButton(StatEnum stat, int current, int max)
    {
        switch (stat)
        {
            case StatEnum.Health:
                {
                    SetupButton(m_healthButton, current, max);
                    break;
                }
            case StatEnum.Attack:
                {
                    SetupButton(m_attackButton, current, max);
                    break;
                }
            case StatEnum.Defence:
                {
                    SetupButton(m_defenceButton, current, max);
                    break;
                }
            case StatEnum.Energy:
                {
                    SetupButton(m_energyButton, current, max);
                    break;
                }
            case StatEnum.Recovery:
                {
                    SetupButton(m_recoveryButton, current, max);
                    break;
                }
        }
    }

    private void SetupButton(StatButtonSetup button, int current, int max)
    {
        button.statButton.Setup(button.statButtonValues, current, max);
    }
}
