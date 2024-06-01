using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Option
{
    public OptionType optionType;
    public float value;

    public string GetOptionTypeString()
    {
        switch (optionType)
        {
            case OptionType.FlatATK:
            case OptionType.ATK:
                return "공격력";
            case OptionType.FlatDEF:
            case OptionType.DEF:
                return "방어력";
            case OptionType.FlatHP:
            case OptionType.HP:
                return "HP";
            case OptionType.CritRate:
                return "치명타 확률";
            case OptionType.CritDMG:
                return "치명타 피해";
            case OptionType.Healing:
                return "치유량 증가";
            default:
                return string.Empty;
        }
    }
    public string GetOptionValueString()
    {
        switch (optionType)
        {
            case OptionType.ATK:
            case OptionType.DEF:
            case OptionType.HP:
            case OptionType.CritRate:
            case OptionType.CritDMG:
                return $"{value}%";
            default:
                return $"{value}";
        }
    }
    public string GetAllOptionString()
    {
        return GetOptionTypeString() + "+" + GetOptionValueString();
    }
}

public enum OptionType
{
    ATK,
    DEF,
    HP,
    CritRate,
    CritDMG,
    Healing,
    FlatDEF,
    FlatATK,
    FlatHP
}