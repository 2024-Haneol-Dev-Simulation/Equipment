using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public string HeadEquipEquipmentInstanceID;
    public string WingEquipEquipmentInstanceID;
    public string MissleEquipEquipmentInstanceID;
    public string EngineEquipEquipmentInstanceID;
    public List<MyEquipment> InventoryEquipment;

    public void Reset()
    {
        

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
}
