using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : ScriptableObject
{
    public new string name;
    public Sprite image;
    //set
    public string experience;

    public string GetEquipmentType()
    {
        if (this.GetType() == typeof(EquipmentHead))
        {
            return "머리";
        }
        else if (this.GetType() == typeof(EquipmentWing))
        {
            return "날개";
        }
        else if (this.GetType() == typeof(EquipmentMissle))
        {
            return "미사일";
        }
        else
        {
            return "엔진";
        }
    }
}