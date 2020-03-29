using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtonExplore : ActionButtonReward
{
    public void OnEnable()
    {
        m_text.text = "Explore";
    }

    public override void OnClick()
    {
        if(GameManager.Instance.TryTakeStat(StatEnum.Energy, m_cost))
        {
            TakeDamage();            
        }
    }
}
