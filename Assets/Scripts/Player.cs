using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum StatEnum
{
    None,
    Health,
    Attack,
    Defence,
    Energy,
    Recovery
}

[System.Serializable]
public class Stat
{
    public StatEnum StatType{ get { return statType; } }

    //ADD BASE VALUE SO WE CAN CALCULATE MAX FROM BASE + ARMOUR + TALENT POINTS
    public float MaxStat { get { return maxStat; } }
    public float CurrentStat { get { return currentStat; } }
    public bool AlwaysMax {  get { return alwaysMax; } }

    [HideInInspector] public UnityEvent OnStatChange;

    [SerializeField] private StatEnum statType;
    [SerializeField] private float maxStat;
    [SerializeField] private float currentStat;
    [SerializeField] float startRecoveryTime;
    [SerializeField] bool alwaysMax;

    private bool isRecovering;
    private float startRecoveryProgress;

    public float GetPercentage()
    {
        return alwaysMax ? 1 : CurrentStat / MaxStat;
    }

    public void ChangeCurrentStat(float amount)
    {
        if (!alwaysMax)
        {
            currentStat = Mathf.Clamp(currentStat + amount, 0, maxStat);

            OnStatChange.Invoke();

            if (amount < 0)
            {
                ResetRecoveryProgress();
            }
        }
    }

    public void SetMaxStat(float amount)
    {
        maxStat = amount;

        if(alwaysMax)
        {
            currentStat = maxStat;
        }

        OnStatChange.Invoke();
    }

    public void RecoverStat(float amount)
    {
        if (!alwaysMax)
        {
            if (isRecovering)
            {
                if (CurrentStat < MaxStat)
                {
                    ChangeCurrentStat(amount);
                }
            }
            else
            {
                startRecoveryProgress += Time.deltaTime / startRecoveryTime;

                if (startRecoveryProgress >= 1)
                {
                    isRecovering = true;
                }
            }
        }
    }

    private void ResetRecoveryProgress()
    {
        if (!alwaysMax)
        {
            startRecoveryProgress = 0;
            isRecovering = false;
        }
    }
}

[System.Serializable]
public class Value
{
    public int Amount { get { return amount; } }

    [SerializeField] int amount;

    public void ChangeAmount(int amount)
    {
        this.amount += amount;
    }
}

[System.Serializable]
public class Stats
{
    public Stat health;
    public Stat attack;
    public Stat defence;
    public Stat energy;
    public Stat recovery;
    public bool Dead { get { return dead; } }

    public Value xp;

    private bool dead;

    public void SetCurrentToMax()
    {
        health.ChangeCurrentStat(health.MaxStat);
        attack.ChangeCurrentStat(attack.MaxStat);
        defence.ChangeCurrentStat(defence.MaxStat);
        energy.ChangeCurrentStat(energy.MaxStat);
        recovery.ChangeCurrentStat(recovery.MaxStat);
    }

    public void RecoverStats()
    {
        if (!dead)
        {
            float amount = recovery.CurrentStat * Time.deltaTime;
            health.RecoverStat(amount);
            energy.RecoverStat(amount);
        }
    }

    public void SetDead(bool isDead = true)
    {
        dead = isDead;
    }
}

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats : Stats
    {
        public Value level;

        public Value hardCurrency;
        public Value softCurrency;

        public Value softKey;
        public Value hardKey;
    }

    [SerializeField] PlayerStats m_stats;

    public PlayerStats GetStats()
    {
        return m_stats;
    }

    public Stat GetStat(StatEnum statType)
    {
        switch (statType)
        {
            case StatEnum.Health:
                {
                    return m_stats.health;
                }
            case StatEnum.Attack:
                {
                    return m_stats.attack;
                }
            case StatEnum.Defence:
                {
                    return m_stats.defence;
                }
            case StatEnum.Energy:
                {
                    return m_stats.energy;
                }
            case StatEnum.Recovery:
                {
                    return m_stats.recovery;
                }
            default:
                {
                    return null;
                }
        }
    }

    private void Start()
    {
        SetupStats();
    }

    private void SetupStats()
    {
        m_stats.health.OnStatChange.AddListener(() => GameManager.Instance.UpdateStatButton(m_stats.health));
        m_stats.energy.OnStatChange.AddListener(() => GameManager.Instance.UpdateStatButton(m_stats.energy));
    }

    private void Update()
    {
        m_stats.RecoverStats();
    }

    public void GainXP(int amount)
    {
        m_stats.xp.ChangeAmount(amount);

        while (m_stats.xp.Amount >= GameManager.GetXPForLevel(m_stats.level.Amount + 1))
        {
            LevelUp();
        }

        GameManager.Instance.UpdateXPBar(m_stats);
    }

    private void LevelUp()
    {
        m_stats.level.ChangeAmount(1);
        GameManager.Instance.UpdateTopBar(TopBar.UI.Level, m_stats.level);
    }

    public void AttackTarget(Enemy target)
    {
        target.TakeDamage(m_stats.attack.CurrentStat);
    }

    public void TakeDamage(float amount)
    {
        m_stats.health.ChangeCurrentStat(-amount);

        if(m_stats.health.CurrentStat <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        m_stats.SetDead();
    }

    public bool TryTakeStat(StatEnum statType, float amount)
    {
        Stat stat = GetStat(statType);        

        if(stat == null || stat.CurrentStat < amount)
        {
            return false;
        }

        stat.ChangeCurrentStat(-amount);
        return true;
    }

#if CHEATS
    [ContextMenu("AddXP")]
    public void AddXP()
    {
        GainXP(1);
    }
#endif
}
