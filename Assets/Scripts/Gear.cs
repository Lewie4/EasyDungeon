using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gear
{
    public enum Slot
    {
        None,
        Weapon,
        Head,
        Chest,
        Feet,
        Ring,
        Neck
    }

    [System.Serializable]
    public class GearStats
    {
        public float m_health;
        public float m_attack;
        public float m_defence;
        public float m_energy;
        public float m_recovery;

        public static GearStats AddStats(GearStats stat1, GearStats stat2)
        {
            GearStats newStats = new GearStats();
            newStats.m_health = stat1.m_health + stat2.m_health;
            newStats.m_attack = stat1.m_attack + stat2.m_attack;
            newStats.m_defence = stat1.m_defence + stat2.m_defence;
            newStats.m_energy = stat1.m_energy + stat2.m_energy;
            newStats.m_recovery = stat1.m_recovery + stat2.m_recovery;

            return newStats;
        }
    }

    public Slot slot { get { return m_slot; } }

    [SerializeField] Slot m_slot;
    [SerializeField] int m_seed;
    [SerializeField] int m_level;
    [SerializeField] GearStats m_stats;

    private int m_setup = -1;

    public GearStats GetStats()
    {
        if (m_setup != m_seed)
        {
            Random.InitState(m_seed);

            m_stats.m_health = Random.Range(0f, m_level * 5f);
            m_stats.m_attack = Random.Range(0f, m_level * 5f);
            m_stats.m_defence = Random.Range(0f, m_level * 5f);
            m_stats.m_energy = Random.Range(0f, m_level * 5f);
            m_stats.m_recovery = Random.Range(0f, m_level * 5f);
        }

        return m_stats;
    }
}
