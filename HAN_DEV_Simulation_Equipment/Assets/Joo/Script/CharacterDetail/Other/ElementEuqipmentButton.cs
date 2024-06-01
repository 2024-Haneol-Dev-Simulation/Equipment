using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class ElementEuqipmentButton : MonoBehaviour
{
    [SerializeField] private Image euqipmentBackgroundImage;
    [SerializeField] private Image euqipmentImage;
    [SerializeField] private Image EquipEquipment;
    [SerializeField] private Image LightSelect;
    [SerializeField] private TMP_Text enhancementText;
    [SerializeField] private Button cancelButton;

    private MyEquipment equipment;
    private bool selectButton;
    public void InitButton(MyEquipment myEquipment,UnityAction clickAction, UnityAction cancelAction = null)
    {
        equipment = myEquipment;
        enhancementText.text = $"+{myEquipment.enhancement}";

        euqipmentBackgroundImage.gameObject.SetActive(true);
        if(myEquipment.equipmentClass == EquipmentClass.Rare)
            euqipmentBackgroundImage.color = new Color32(104, 92, 144, 255);
        else
            euqipmentBackgroundImage.color = new Color32(214, 167, 100, 255);

        euqipmentImage.sprite = myEquipment.equipment.image;


        GetComponent<Button>().onClick.AddListener(clickAction);
        if(cancelAction != null)
        {
            AddCancelAction(cancelAction);
        }
    }
    public void Clear()
    {
        equipment = null;
        enhancementText.text = "--";
        euqipmentBackgroundImage.gameObject.SetActive(false);

        GetComponent<Button>().onClick.RemoveAllListeners();

        cancelButton.gameObject.SetActive(false);
        cancelButton.onClick.RemoveAllListeners();
    }
    public bool CheackEqualsequipment(MyEquipment item)
    {
        if (equipment == item)
            return true;
        else
            return false;
    }
    public void Select()
    {
        selectButton = true;
        LightSelect.DOFade(1, 0.2f);
    }
    public void UnSelect()
    {
        selectButton = false;
        LightSelect.DOFade(0, 0.2f);
    }
    public void Equip()
    {
        EquipEquipment.gameObject.SetActive (true);
    }
    public void UnEquip()
    {
        EquipEquipment.gameObject.SetActive(false);
    }
    public void AddCancelAction(UnityAction cancelAction)
    {
        cancelButton.gameObject.SetActive(true);
        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(cancelAction);

        cancelButton.onClick.AddListener(() =>
        {
            cancelButton.gameObject.SetActive(false);
        });
    }
    public bool CheakSelect()
    {
        return selectButton;
    }
    public void Reset()
    {
        enhancementText.text = "--";
        euqipmentBackgroundImage.gameObject.SetActive(false);

        GetComponent<Button>().onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        cancelButton.gameObject.SetActive(false);
    }

    
}
