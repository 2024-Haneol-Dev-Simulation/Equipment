using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitEquimentListUI : MonoBehaviour
{
    [SerializeField] private EquipmentList equipmentList;
    [SerializeField] private EquipmentSystem equipmentSystem;
    [SerializeField] private GameObject EngineEquipmentObject;
    [SerializeField] private GameObject equipmentAttribute;
    [SerializeField] private CharacterEquimentButton characterEquimentButton;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            characterEquimentButton.AllHideButton(0);
            equipmentSystem.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(Selct());
        });
    }
    private IEnumerator Selct()
    {
        yield return new WaitForEndOfFrame();
        if (EngineEquipmentObject.transform.childCount == 0)
        {
            equipmentAttribute.gameObject.SetActive(false);
        }
        else
        {
            
            equipmentAttribute.gameObject.SetActive(true);
            //equipmentAttribute.Getty
            if (equipmentSystem.CheckEquipEquipment(typeof(EquipmentEngine)))
            {
                //장착할 장비가 있음
                equipmentList.SelectEquipment(equipmentSystem.GetEquipEquipment(typeof(EquipmentEngine)));
            }
            else
            {
                equipmentList.SelectEquipment(typeof(EquipmentEngine), 0);
            }
        }
    }
}
