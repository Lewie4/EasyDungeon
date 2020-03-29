using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtonReward : ActionButton
{
    [SerializeField] protected int m_cost;
    [SerializeField] protected int m_damagePerCost;
    [SerializeField] protected int m_currentHealth;
    [SerializeField] protected int m_maxHealth;
    //Reward to give

    protected void TakeDamage()
    {
        m_currentHealth -= m_damagePerCost;
        SetFill((float)m_currentHealth / m_maxHealth);
    }
}
