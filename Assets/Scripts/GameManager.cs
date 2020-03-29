using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Player m_player;

    [SerializeField] XPBar m_xpBar;
    [SerializeField] StatButtons m_statButtons;    
    [SerializeField] TopBar m_topBar;

    static readonly float m_xpPower = 2;

    public static int GetXPForLevel(int level)
    {
        return Mathf.RoundToInt(Mathf.Pow(level, m_xpPower)) * 100;
    }

    public void Start()
    {
        SetupUI();
    }

    private void SetupUI()
    {
        Player.PlayerStats playerStats = m_player.GetStats();

        UpdateXPBar(playerStats);
        UpdateStatButtons(playerStats);
        UpdateFullTopBar(playerStats);
    }

    public void UpdateXPBar(Player.PlayerStats playerStats)
    {
        int xpIntoLevel = playerStats.xp.Amount - GetXPForLevel(playerStats.level.Amount);
        int xpForNext = GetXPForLevel(playerStats.level.Amount + 1) - GetXPForLevel(playerStats.level.Amount);

        Debug.Log(xpIntoLevel + "/" + xpForNext);
        m_xpBar.ChangeSliderValue(xpIntoLevel, xpForNext);
    }

    public void UpdateStatButtons(Player.PlayerStats playerStats)
    {
        UpdateStatButton(playerStats.health);
        UpdateStatButton(playerStats.attack);
        UpdateStatButton(playerStats.defence);
        UpdateStatButton(playerStats.energy);
        UpdateStatButton(playerStats.recovery);
    }

    public void UpdateStatButton(Stat statValue)
    {
        m_statButtons.SetupButton(statValue);
    }

    public void UpdateFullTopBar(Player.PlayerStats playerStats)
    {
        UpdateTopBar(TopBar.UI.HardCurrency, playerStats.hardCurrency);
        UpdateTopBar(TopBar.UI.SoftCurrency, playerStats.softCurrency);
        UpdateTopBar(TopBar.UI.Level, playerStats.level);
        UpdateTopBar(TopBar.UI.SoftKey, playerStats.softKey);
        UpdateTopBar(TopBar.UI.HardKey, playerStats.hardKey);
    }

    public void UpdateTopBar(TopBar.UI uiElement, Value value, Sprite sprite = null)
    {
        m_topBar.UpdateUI(uiElement, value.Amount.ToString(), sprite);
    }

    public void TryAttackEnemy(Enemy enemy)
    {
        Player.PlayerStats playerStats = m_player.GetStats();

        if(!playerStats.Dead)
        {
            m_player.AttackTarget(enemy);
        }
    }

    public void DamagePlayer(float amount)
    {
        m_player.TakeDamage(amount);
    }

    public void GivePlayerXP(int amount)
    {
        m_player.GainXP(amount);
    }

    public bool TryTakeStat(StatEnum statType, float amount)
    {
        return m_player.TryTakeStat(statType, amount);
    }
}
