using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatButtons : MonoBehaviour
{
    [System.Serializable]
    public class StatButtonSetup
    {
        public StatButton statButton;
        public StatInfo statInfo;
    }

    [SerializeField] StatButtonSetup m_healthButton;
    [SerializeField] StatButtonSetup m_attackButton;
    [SerializeField] StatButtonSetup m_defenceButton;
    [SerializeField] StatButtonSetup m_energyButton;
    [SerializeField] StatButtonSetup m_recoveryButton;

    public void Start()
    {

    }

    public void SetupButton(Stat stat)
    {
        switch (stat.StatType)
        {
            case StatEnum.Health:
                {
                    SetupButton(m_healthButton, stat);
                    break;
                }
            case StatEnum.Attack:
                {
                    SetupButton(m_attackButton, stat);
                    break;
                }
            case StatEnum.Defence:
                {
                    SetupButton(m_defenceButton, stat);
                    break;
                }
            case StatEnum.Energy:
                {
                    SetupButton(m_energyButton, stat);
                    break;
                }
            case StatEnum.Recovery:
                {
                    SetupButton(m_recoveryButton, stat);
                    break;
                }
        }
    }

    private void SetupButton(StatButtonSetup button, Stat stat)
    {
        button.statButton.Setup(button.statInfo, stat);
    }
}
