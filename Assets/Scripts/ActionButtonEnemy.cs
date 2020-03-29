using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtonEnemy : ActionButton
{
    public Enemy m_enemy;

    private void Start()
    {
        SetupStats();
    }

    private void SetupStats()
    {
        var stats = m_enemy.GetStats();        
        stats.health.OnStatChange.AddListener(() => SetFill(stats.health.GetPercentage()));
    }

    public void OnEnable()
    {
        m_text.text = "Attack";
    }

    public override void OnClick()
    {
        GameManager.Instance.TryAttackEnemy(m_enemy);
    }
}
