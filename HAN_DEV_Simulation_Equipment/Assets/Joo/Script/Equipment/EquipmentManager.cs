using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using static EnhancementAdditionalOption;

[CreateAssetMenu(fileName = "EquipmentManager", menuName = "Equipment/AllEquipment", order = 5)]
public class EquipmentManager : ScriptableObject
{
    public List<Equipment> allEquipmentList;
    private DatabaseManager database;
    private DatabaseManager databaseManager
    {
        get
        {
            if (database == null)
                database = new DatabaseManager();

            return database;
        }
    }
    //랜덤한 아이템
    public MyEquipment GetRandomEquipment()
    {
        MyEquipment myEquipment = new MyEquipment();
        myEquipment.UID = GUID.Generate().ToString();
        myEquipment.equipment = allEquipmentList[Random.Range(0, allEquipmentList.Count)];

        myEquipment.equipmentClass = (EquipmentClass)Random.Range(0, 2);

        Option option = new Option();
        option.optionType = GetRandomMainOption(myEquipment.equipment.GetType());
        option.value = databaseManager.GetMainOption(option.optionType, 0);
        myEquipment.mainOption = option;

        int optionCount = Random.Range(3, 5);
        myEquipment.additionalOption = new List<Option>();
        for (int i = 0;i<optionCount;i++)
        {
            myEquipment.additionalOption.Add(GetNewAdditionalOption(myEquipment.mainOption.optionType, myEquipment.additionalOption, myEquipment.equipmentClass));
        }
        myEquipment.enhancement = 0;
        myEquipment.enhancementExperience = 0;

        return myEquipment;
    }
    public OptionType GetRandomMainOption(System.Type Equipmenttype)
    {
        if (Equipmenttype == typeof(EquipmentHead))
        {
            return (OptionType)Random.Range(0, 6);
        }
        else if (Equipmenttype == typeof(EquipmentWing))
        {
            return (OptionType)Random.Range(0, 3);
        }
        else if (Equipmenttype == typeof(EquipmentMissle))
        {
            return OptionType.FlatATK;
        }
        else
        {
            return OptionType.FlatHP;
        }
    }
    public float GetMainOption(OptionType type, int level)
    {
        return databaseManager.GetMainOption(type, level);
    } 
    public void AddAdditionalOption(MyEquipment myEquipment, int loopCount = 1, EventChangeAdditionalOption eventChangeAdditionalOption = null)
    {
        while (loopCount > 0)
        {
            if (myEquipment.additionalOption.Count >= 4)
            {
                Option option = GetOldAdditionalOption(
                    myEquipment.mainOption.optionType,
                    myEquipment.additionalOption,
                    myEquipment.equipmentClass);

                int index = FindOption(myEquipment.additionalOption, option.optionType);
                if(eventChangeAdditionalOption!=null)
                    eventChangeAdditionalOption(index);
                myEquipment.additionalOption[index].value += option.value;
            }
            else
            {
                myEquipment.additionalOption.Add(GetNewAdditionalOption(
                    myEquipment.mainOption.optionType,
                    myEquipment.additionalOption,
                    myEquipment.equipmentClass));
            }

            loopCount--;
        }
        
    }
    public Option GetNewAdditionalOption(OptionType mainOption,List<Option> duplicationOption, EquipmentClass equipmentClass)
    {
        Option option = new Option();
        do
        {
            option.optionType = (OptionType)Random.Range(0, 9);
        } while (OptionContain(duplicationOption, option.optionType) || option.optionType == mainOption || option.optionType == OptionType.Healing);

        string size = string.Empty;

        switch(Random.Range(0,4))
        {
            case 0:
                size = "Lowest";
                break;
            case 1:
                size = "Low";
                break;
            case 2:
                size = "High";
                break;
            case 3:
                size = "Best";
                break;
        }

        option.value = databaseManager.GetAdditionalOption(option.optionType, equipmentClass, size);

        return option;
    }
    public Option GetOldAdditionalOption(OptionType mainOption, List<Option> duplicationOption, EquipmentClass equipmentClass)
    {
        Option option = new Option();
        do
        {
            option.optionType = (OptionType)Random.Range(0, 9);
        } while (!OptionContain(duplicationOption, option.optionType) || option.optionType == mainOption || option.optionType == OptionType.Healing);

        string size = string.Empty;

        switch (Random.Range(0, 4))
        {
            case 0:
                size = "Lowest";
                break;
            case 1:
                size = "Low";
                break;
            case 2:
                size = "High";
                break;
            case 3:
                size = "Best";
                break;
        }

        option.value = databaseManager.GetAdditionalOption(option.optionType, equipmentClass, size);

        return option;
    }
    public int GetEquipmentNeedExperience(EquipmentClass equipmentClass, int EnhancementCount)
    {
        return databaseManager.GetEquipmentNeedExperience(equipmentClass, EnhancementCount);
    }
    public List<int> GetEquipmentNeedExperience(EquipmentClass equipmentClass)
    {
        return databaseManager.GetEquipmentNeedExperience(equipmentClass);
    }
    private bool OptionContain(List<Option> options, OptionType targetOption)
    {
        foreach (Option option in options)
        {
            if(option.optionType == targetOption)
                return true;
        }

        return false;
    }
    private int FindOption(List<Option> options, OptionType targetOption)
    {
        for (int i = 0; i < options.Count; i++)
        {
            if (options[i].optionType == targetOption)
                return i;
        }
        Debug.LogError("Not Contain Option");
        return -1;
    }
}


