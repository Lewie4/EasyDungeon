using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGear : MonoBehaviour
{
    [System.Serializable]
    public class Outfit
    {
        public Gear Weapon { get { return weapon; } }
        public Gear Head { get { return head; } }
        public Gear Chest { get { return chest; } }
        public Gear Feet { get { return feet; } }
        public Gear Ring { get { return ring; } }
        public Gear Neck { get { return neck; } }
        public Gear.GearStats TotalStats { get { return CalculateTotalStats(); } }

        [SerializeField] Gear weapon;
        [SerializeField] Gear head;
        [SerializeField] Gear chest;
        [SerializeField] Gear feet;
        [SerializeField] Gear ring;
        [SerializeField] Gear neck;

        public Gear GetGear(Gear.Slot targetSlot)
        {
            switch (targetSlot)
            {
                case Gear.Slot.Weapon:
                    {
                        return weapon;
                    }
                case Gear.Slot.Head:
                    {
                        return head;
                    }
                case Gear.Slot.Chest:
                    {
                        return chest;
                    }
                case Gear.Slot.Feet:
                    {
                        return feet;
                    }
                case Gear.Slot.Ring:
                    {
                        return ring;
                    }
                case Gear.Slot.Neck:
                    {
                        return neck;
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        public bool ChangeGear(Gear gear, Gear.Slot targetSlot)
        {
            if (gear.slot != targetSlot)
            {
                return false;
            }

            switch (targetSlot)
            {
                case Gear.Slot.Weapon:
                    {
                        weapon = gear;
                        break;
                    }
                case Gear.Slot.Head:
                    {
                        head = gear;
                        break;
                    }
                case Gear.Slot.Chest:
                    {
                        chest = gear;
                        break;
                    }
                case Gear.Slot.Feet:
                    {
                        feet = gear;
                        break;
                    }
                case Gear.Slot.Ring:
                    {
                        ring = gear;
                        break;
                    }
                case Gear.Slot.Neck:
                    {
                        neck = gear;
                        break;
                    }
                default:
                    {
                        return false;
                    }
            }
            return true;
        }

        private Gear.GearStats CalculateTotalStats()
        {
            Gear.GearStats total = new Gear.GearStats();
            total = Gear.GearStats.AddStats(total, weapon.GetStats());
            total = Gear.GearStats.AddStats(total, head.GetStats());
            total = Gear.GearStats.AddStats(total, chest.GetStats());
            total = Gear.GearStats.AddStats(total, feet.GetStats());
            total = Gear.GearStats.AddStats(total, ring.GetStats());
            total = Gear.GearStats.AddStats(total, neck.GetStats());

            return total;
        }
    }

    [SerializeField] private Outfit m_outfit;

    private void Start()
    {
        
    }

    public Gear.GearStats GetTotalStats()
    {
        return m_outfit.TotalStats;
    }

    public Gear ChangeGear(Gear gear, Gear.Slot targetSlot)
    {
        Gear oldGear = m_outfit.GetGear(targetSlot);

        if(m_outfit.ChangeGear(gear, targetSlot))
        {
            return oldGear;
        }

        return gear;
    }
}
