using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentSet", menuName = "Equipment/EquipmentSet", order = 4)]
public class EquipmentSetData : ScriptableObject
{
    public List<EquipmentSet> equipmentSets;

    public void GetAllEquipmentSet(List<Equipment> equipment)
    {
        List<MyEquipmentSet> myEquipSet = new List<MyEquipmentSet>();
        for (int i = 0; i < equipmentSets.Count; i++)
        {
            int setCount = 0;
            for(int j = 0;j < equipment.Count;j++)
            {
                if (equipmentSets[i].equipment.Contains(equipment[j]))
                {
                    setCount++;
                }
            }
            if(setCount > 0)
            {
                myEquipSet.Add(new MyEquipmentSet(equipmentSets[i], setCount));
            }

        }

        foreach(var equipmentSet in myEquipSet)
        {
            Debug.Log($"{equipmentSet.equipmentSet.setName} : {equipmentSet.activeCount}");
        }
    }
    public MyEquipmentSet GetEquipmentSet(Equipment targetEquipment, List<Equipment> equipment)
    {
        int i;
        for (i = 0; i < equipmentSets.Count; i++)
        {
            if (equipmentSets[i].equipment.Contains(targetEquipment))
            {
                break;
            }

        }

        int setCount = 0;
        for (int j = 0; j < equipment.Count; j++)
        {
            if (equipmentSets[i].equipment.Contains(equipment[j]))
            {
                setCount++;
            }
        }

        return new MyEquipmentSet(equipmentSets[i], setCount);
    }
}
public class MyEquipmentSet
{
    public EquipmentSet equipmentSet;
    public int activeCount;

    public MyEquipmentSet(EquipmentSet equipmentSet, int activeCount)
    {
        this.equipmentSet = equipmentSet;
        this.activeCount = activeCount;
    }
}
[Serializable]
public class EquipmentSet
{
    public string setName;
    public Option Set2option;
    public string Set4option;
    public List<Equipment> equipment;
}
