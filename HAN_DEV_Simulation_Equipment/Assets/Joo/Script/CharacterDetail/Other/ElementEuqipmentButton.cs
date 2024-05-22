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
    [SerializeField] private TMP_Text enhancementText;
    [SerializeField] private Button cancelButton;

    private MyEquipment equipment;
    public void InitButton(MyEquipment myEquipment,UnityAction clickAction, UnityAction cancelAction = null)
    {
        equipment = myEquipment;
        enhancementText.text = $"+{myEquipment.enhancement}";

        euqipmentBackgroundImage.gameObject.SetActive(true);
        euqipmentBackgroundImage.color = Color.yellow;


        GetComponent<Button>().onClick.AddListener(clickAction);
        if(cancelAction != null)
        {
            cancelButton.gameObject.SetActive(true);
            cancelButton.onClick.AddListener(cancelAction);
        }
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
