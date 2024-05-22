using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MyEquipment
{
    public string UID;
    public Equipment equipment;

    public Option mainOption;
    public List<Option> additionalOption;

    public EquipmentClass equipmentClass;

    public int enhancement;
}

public enum EquipmentClass
{
    Nomal,
    Rare,
    Legend
}