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
                return "���ݷ�";
            case OptionType.FlatDEF:
            case OptionType.DEF:
                return "����";
            case OptionType.FlatHP:
            case OptionType.HP:
                return "HP";
            case OptionType.CritRate:
                return "ġ��Ÿ Ȯ��";
            case OptionType.CritDMG:
                return "ġ��Ÿ ����";
            case OptionType.Healing:
                return "ġ���� ����";
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