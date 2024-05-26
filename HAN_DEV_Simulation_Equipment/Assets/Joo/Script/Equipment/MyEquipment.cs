using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MyEquipment
{
    public string UID;
    public Equipment equipment;

    public EquipmentClass equipmentClass;

    public Option mainOption;
    public List<Option> additionalOption;


    public int enhancement;
    public int enhancementExperience;
}

public enum EquipmentClass
{
    Rare,
    Legend
}