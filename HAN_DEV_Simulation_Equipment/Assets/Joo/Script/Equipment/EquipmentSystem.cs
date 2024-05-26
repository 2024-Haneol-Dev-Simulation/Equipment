using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] private EquipmentSetData equipmentSetData;
    [SerializeField] private EquipmentManager equipmentManager;
    [SerializeField] protected PlayerData playerData;

    public string SelectEquipmentID;
    void Start()
    {
        playerData.Reset();
        for(int i = 0;i<30;i++)
        {
            playerData.InventoryEquipment.Add(equipmentManager.GetRandomEquipment());
        }
        equipmentSetData.GetAllEquipmentSet(playerData.GetEquipEquipment());
    }
    public bool CheckEquipEquipment(Type type)
    {
        if (type == typeof(EquipmentHead))
        {
            if (playerData.HeadEquipEquipmentInstanceID == string.Empty)
                return false;
            else 
                return true;
        }
        else if (type == typeof(EquipmentWing))
        {
            if (playerData.WingEquipEquipmentInstanceID == string.Empty)
                return false;
            else
                return true;
        }
        else if (type == typeof(EquipmentMissle))
        {
            if (playerData.MissleEquipEquipmentInstanceID == string.Empty)
                return false;
            else
                return true;
        }
        else if (type == typeof(EquipmentEngine))
        {
            if (playerData.EngineEquipEquipmentInstanceID == string.Empty)
                return false;
            else
                return true;
        }
        
        return false;
    }
    public bool EqualsEquipEquipment(Type type,string uid)
    {
        if (type == typeof(EquipmentHead))
        {
            if (playerData.HeadEquipEquipmentInstanceID == uid)
                return true;
            else
                return false;
        }
        else if (type == typeof(EquipmentWing))
        {
            if (playerData.WingEquipEquipmentInstanceID == uid)
                return true;
            else
                return false;
        }
        else if (type == typeof(EquipmentMissle))
        {
            if (playerData.MissleEquipEquipmentInstanceID == uid)
                return true;
            else
                return false;
        }
        else if (type == typeof(EquipmentEngine))
        {
            if (playerData.EngineEquipEquipmentInstanceID == uid)
                return true;
            else
                return false;
        }

        return false;
    }
    public MyEquipmentSet GetEquipmentSet(Equipment equipment)
    {
        return equipmentSetData.GetEquipmentSet(equipment, playerData.GetEquipEquipment());
    }

    public MyEquipment GetEquipEquipment(Type type)
    {
        if (type == typeof(EquipmentHead))
        {
            return playerData.GetEquipment(playerData.HeadEquipEquipmentInstanceID);
        }
        else if (type == typeof(EquipmentWing))
        {
            return playerData.GetEquipment(playerData.WingEquipEquipmentInstanceID);
        }
        else if (type == typeof(EquipmentMissle))
        {
            return playerData.GetEquipment(playerData.MissleEquipEquipmentInstanceID);
        }
        else
        {
            return playerData.GetEquipment(playerData.EngineEquipEquipmentInstanceID);
        }
    }
    public void SetEquipEquipment(MyEquipment myEquipment)
    {
        playerData.SetEquipEquipment(myEquipment.equipment.GetType(), myEquipment.UID);
    }
    public void RemoveEquipEquipment(Type type)
    {
        playerData.RemoveEquipEquipment(type);
    }
    public MyEquipment GetEquipment(string id)
    {
        return playerData.GetEquipment(id);
    }
    public Stats GetEquipStats()
    {
        return playerData.GetEquipStats();
    }
    public Stats GetAllStats()
    {
        return playerData.GetAllStats();
    }
    public List<MyEquipmentSet> GetAllEquipmentSet()
    {
        return equipmentSetData.GetAllEquipmentSet(playerData.GetEquipEquipment());
    }
}
