using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Stat")]
    public Stats BasicStats;
    private Stats EquipStats;
    [Space(10)]
    public string HeadEquipEquipmentInstanceID;
    public string WingEquipEquipmentInstanceID;
    public string MissleEquipEquipmentInstanceID;
    public string EngineEquipEquipmentInstanceID;
    public List<MyEquipment> InventoryEquipment;

    public void DataReset()
    {
        EquipStats = new Stats();
        EquipStats.Reset();

        HeadEquipEquipmentInstanceID = string.Empty;
        WingEquipEquipmentInstanceID = string.Empty;
        MissleEquipEquipmentInstanceID = string.Empty;
        EngineEquipEquipmentInstanceID = string.Empty;

        InventoryEquipment = new List<MyEquipment>();
    }
    public List<Equipment> GetEquipEquipment()
    {
        List <Equipment> equip = new List <Equipment>();
        
        if(ContainEquipment(HeadEquipEquipmentInstanceID))
            equip.Add(GetEquipment(HeadEquipEquipmentInstanceID).equipment);
        if(ContainEquipment(WingEquipEquipmentInstanceID))
            equip.Add(GetEquipment(WingEquipEquipmentInstanceID).equipment);
        if(ContainEquipment(MissleEquipEquipmentInstanceID))
            equip.Add(GetEquipment(MissleEquipEquipmentInstanceID).equipment);
        if(ContainEquipment(EngineEquipEquipmentInstanceID))
            equip.Add(GetEquipment(EngineEquipEquipmentInstanceID).equipment);

        return equip;
    }
    public void SetEquipEquipment(Type type, string uid)
    {
        
        if (type == typeof(EquipmentHead))
        {
            HeadEquipEquipmentInstanceID = uid;
        }
        else if (type == typeof(EquipmentWing))
        {
            WingEquipEquipmentInstanceID = uid;
        }
        else if (type == typeof(EquipmentMissle))
        {
            MissleEquipEquipmentInstanceID = uid;
        }
        else
        {
            EngineEquipEquipmentInstanceID = uid;
        }
    }
    public bool ContainEquipment(string InstanceID)
    {
        if (InstanceID == string.Empty)
            return false;

        for (int i = 0; i < InventoryEquipment.Count; i++)
        {
            if (InstanceID == InventoryEquipment[i].UID)
                return true;
        }
        return false;
    }
    public void RemoveEquipment(string InstanceID)
    {
        for (int i = 0; i < InventoryEquipment.Count; i++)
        {
            if (InstanceID == InventoryEquipment[i].UID)
            {
                InventoryEquipment.RemoveAt(i);
                return;
            }
        }
    }
    public MyEquipment GetEquipment(string InstanceID)
    {
        if(InstanceID == string.Empty)
            return null;

        for(int i = 0; i < InventoryEquipment.Count; i++)
        {
            if (InstanceID == InventoryEquipment[i].UID)
                return InventoryEquipment[i];
        }
        return null;
    }
    public List<MyEquipment> GetAllInventoryEquipment()
    {
        return InventoryEquipment;
    }
    public void RemoveEquipEquipment(Type type)
    {
        if (type == typeof(EquipmentHead))
        {
            HeadEquipEquipmentInstanceID = string.Empty;
        }
        else if (type == typeof(EquipmentWing))
        {
            WingEquipEquipmentInstanceID = string.Empty;
        }
        else if (type == typeof(EquipmentMissle))
        {
            MissleEquipEquipmentInstanceID = string.Empty;
        }
        else
        {
            EngineEquipEquipmentInstanceID = string.Empty;
        }
    }

    public Stats GetEquipStats()
    {
        List<string> id = new List<string> {
            HeadEquipEquipmentInstanceID ,
            WingEquipEquipmentInstanceID ,
            MissleEquipEquipmentInstanceID,
            EngineEquipEquipmentInstanceID
        };
        EquipStats.Reset();
        foreach (var item in id)
        {
            if (item == string.Empty)
                continue;

            MyEquipment equip = GetEquipment(item);
            AddEquipOption(equip.mainOption);

            for (int i = 0; i<equip.additionalOption.Count;i++)
            {
                AddEquipOption(equip.additionalOption[i]);
            }
        }
        return EquipStats;
    }
    public Stats GetAllStats()
    {
        Stats stats = new Stats();
        GetEquipStats();

        stats.HP = BasicStats.HP + EquipStats.HP;
        stats.ATK = BasicStats.ATK + EquipStats.ATK;
        stats.DEF = BasicStats.DEF + EquipStats.DEF;
        stats.CritRate = BasicStats.CritRate + EquipStats.CritRate;
        stats.CritDMG = BasicStats.CritDMG + EquipStats.CritDMG;
        stats.HealingBonus = BasicStats.HealingBonus + EquipStats.HealingBonus;
        return stats;
    }
    private void AddEquipOption(Option option)
    {
        switch (option.optionType)
        {
            case OptionType.ATK:
                EquipStats.ATK += (int)(BasicStats.ATK * (option.value/100));
                break;
            case OptionType.DEF:
                EquipStats.DEF += (int)(BasicStats.DEF * (option.value / 100));
                break;
            case OptionType.HP:
                EquipStats.HP += (int)(BasicStats.HP * (option.value / 100));
                break;
            case OptionType.CritRate:
                EquipStats.CritRate += option.value;
                break;
            case OptionType.CritDMG:
                EquipStats.CritDMG += option.value;
                break;
            case OptionType.Healing:
                EquipStats.HealingBonus = option.value;
                break;
            case OptionType.FlatDEF:
                EquipStats.DEF += (int)option.value;
                break;
            case OptionType.FlatATK:
                EquipStats.ATK += (int)option.value;
                break;
            case OptionType.FlatHP:
                EquipStats.HP += (int)option.value;
                break;
        }
    }

    
}
[Serializable]
public class Stats
{
    public int HP;
    public int ATK;
    public int DEF;
    public float CritRate;
    public float CritDMG;
    public float HealingBonus;

    public void Reset()
    {
        HP = 0;
        ATK = 0;
        DEF = 0;
        CritRate = 0;
        CritDMG = 0;
        HealingBonus = 0;
    }
}