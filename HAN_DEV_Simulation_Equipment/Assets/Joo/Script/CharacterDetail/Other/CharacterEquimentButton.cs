using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using TMPro;
using System.Reflection;

public class CharacterEquimentButton : UIElement
{
    [SerializeField] private EquipmentSystem equipmentSystem;
    [SerializeField] private List<Button> buttons;

    private List<bool> bools;
    public void Init()
    {
        bools = new List<bool>();

        MyEquipment equipment = equipmentSystem.GetEquipEquipment(typeof(EquipmentEngine));
        bools.Add(SetButton(buttons[0].gameObject, equipment));

        equipment = equipmentSystem.GetEquipEquipment(typeof(EquipmentMissle));
        bools.Add(SetButton(buttons[1].gameObject, equipment));

        equipment = equipmentSystem.GetEquipEquipment(typeof(EquipmentWing));
        bools.Add(SetButton(buttons[2].gameObject, equipment));

        equipment = equipmentSystem.GetEquipEquipment(typeof(EquipmentHead));
        bools.Add(SetButton(buttons[3].gameObject, equipment));
    }
    public void AllHideButton(int index = -1)
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            if(index != i)
            {
                buttons[i].GetComponent<CanvasGroup>().alpha = 0;
                buttons[i].gameObject.SetActive(false);
            }

            
            buttons[i].GetComponent<CanvasGroup>().interactable = false;
            buttons[i].gameObject.transform.GetChild(2).GetComponent<TMP_Text>().color = new Color(1, 1, 1, 0);
            //buttons[i].interactable = false;
        }
    }
    public void AllShowButton()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttons[i].GetComponent<CanvasGroup>().alpha = 1;
            //buttons[i].interactable = true;
            buttons[i].GetComponent<CanvasGroup>().interactable = true;

            if (bools[i])
                buttons[i].gameObject.transform.GetChild(2).GetComponent<TMP_Text>().color = new Color(1, 1, 1, 1);
        }
    }
    public void HideButton(int index)
    {
        buttons[index].GetComponent<CanvasGroup>().DOFade(0, 0.3f).OnComplete(() =>
        {
            buttons[index].gameObject.SetActive(false);
        });
    }
    public void HideButton(Type type)
    {
        int index = Getindex(type);

        HideButton(index);
    }
    public void ShowButton(int index)
    {
        buttons[index].gameObject.SetActive(true);
        buttons[index].GetComponent<CanvasGroup>().DOFade(1, 0.3f);
    }
    public void ShowButton(Type type)
    {
        int index = Getindex(type);

        buttons[index].gameObject.SetActive(true);
        buttons[index].GetComponent<CanvasGroup>().DOFade(1, 0.3f);
    }
    private bool SetButton(GameObject obj,MyEquipment equipment)
    {
        if (equipment != null)
        {
            obj.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            obj.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            obj.gameObject.transform.GetChild(2).gameObject.SetActive(true);

            if (equipment.equipmentClass == EquipmentClass.Rare)
                obj.gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color32(104, 92, 144, 255);
            else
                obj.gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color32(214, 167, 100, 255);

            obj.gameObject.transform.GetChild(1).GetComponent<Image>().sprite = equipment.equipment.image;
            obj.gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = $"+{equipment.enhancement}";

            return true;
        }
        else
        {
            obj.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            obj.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            obj.gameObject.transform.GetChild(2).gameObject.SetActive(false);

            return false;
        }
        
    }
    public void SetButton(MyEquipment equipment)
    {
        int index = Getindex(equipment.equipment.GetType());
        bools[index] = SetButton(buttons[index].gameObject, equipment);
    }
    public void ClearButton(Type type)
    {
        int index = Getindex(type);
        SetButton(buttons[index].gameObject, null);
    }
    private int Getindex(Type type)
    {
        if (type == typeof(EquipmentHead))
            return 3;
        else if (type == typeof(EquipmentWing))
            return 2;
        else if (type == typeof(EquipmentMissle))
            return 1;
        else
            return 0;
    }

    public override void Hide(HideType hideType, Complete complete = null)
    {
        gameObject.SetActive(false);
    }

    public override void Show(ShowType showType, Complete complete = null)
    {
        Init();
        AllShowButton();
        gameObject.SetActive(true);
        if (showType == ShowType.Fade)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        }
    }


}
