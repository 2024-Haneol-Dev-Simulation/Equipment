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
            return "�Ӹ�";
        }
        else if (this.GetType() == typeof(EquipmentWing))
        {
            return "����";
        }
        else if (this.GetType() == typeof(EquipmentMissle))
        {
            return "�̻���";
        }
        else
        {
            return "����";
        }
    }
}