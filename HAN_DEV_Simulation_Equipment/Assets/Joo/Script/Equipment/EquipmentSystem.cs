using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] private EquipmentSetData equipmentSetData;
    [SerializeField] protected PlayerData playerData;
    void Start()
    {
        //playerData.Reset();
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
        if (myEquipment.equipment.GetType() == typeof(EquipmentHead))
        {

            playerData.HeadEquipEquipmentInstanceID = myEquipment.UID;
        }
        else if (myEquipment.equipment.GetType() == typeof(EquipmentWing))
        {
            playerData.WingEquipEquipmentInstanceID = myEquipment.UID;
        }
        else if (myEquipment.equipment.GetType() == typeof(EquipmentMissle))
        {
            playerData.MissleEquipEquipmentInstanceID = myEquipment.UID;
        }
        else
        {
            playerData.EngineEquipEquipmentInstanceID = myEquipment.UID;
        }
    }
    public void RemoveEquipEquipment(Type type)
    {
        if (type == typeof(EquipmentHead))
        {
            playerData.HeadEquipEquipmentInstanceID = string.Empty;
        }
        else if (type == typeof(EquipmentWing))
        {
            playerData.WingEquipEquipmentInstanceID = string.Empty;
        }
        else if (type == typeof(EquipmentMissle))
        {
            playerData.MissleEquipEquipmentInstanceID = string.Empty;
        }
        else
        {
            playerData.EngineEquipEquipmentInstanceID = string.Empty;
        }
    }
}
