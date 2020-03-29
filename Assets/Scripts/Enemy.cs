using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Stats m_stats;

    private void Start()
    {
        SetupStats();        
    }

    private void SetupStats()
    {
        m_stats.SetCurrentToMax();
    }

    private void Update()
    {
        m_stats.RecoverStats();
    }

    public Stats GetStats()
    {
        return m_stats;
    }

    public void TakeDamage(float amount)
    {
        m_stats.health.ChangeCurrentStat(-amount);

        if(m_stats.health.CurrentStat <= 0)
        {
            Die();
        }
        else
        {
            AttackPlayer();
        }
    }

    private void Die()
    {
        GameManager.Instance.GivePlayerXP(m_stats.xp.Amount);
    }

    private void AttackPlayer()
    {
        GameManager.Instance.DamagePlayer(m_stats.attack.CurrentStat);
    }
}
