using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentListMenu : MonoBehaviour
{
    [SerializeField] private EquipmentSystem equipmentSystem;
    [SerializeField] private EquipmentAttribute equipmentAttribute;
    [SerializeField] private EquipmentList equipmentList;
    [SerializeField] private List<Button> buttonList;
    [SerializeField] private List<GameObject> equipmentObjectList;
    [SerializeField] private List<Image> lineList;
    [SerializeField] private CharacterEquimentButton characterEquipmentButton;
    private int selectIndex;
    private void Start()
    {
        buttonList[0].onClick.AddListener(() => { OpenEquipmentList(0); });
        buttonList[1].onClick.AddListener(() => { OpenEquipmentList(1); });
        buttonList[2].onClick.AddListener(() => { OpenEquipmentList(2); });
        buttonList[3].onClick.AddListener(() => { OpenEquipmentList(3); });
    }
    public void OpenEquipmentList(int index)
    {
        if (selectIndex == index)
            return;

        if(selectIndex != -1)
        {
            lineList[selectIndex].transform.DOKill();
            lineList[selectIndex].transform.DOScaleX(0, 0.3f);

            equipmentObjectList[selectIndex].SetActive(false);

            characterEquipmentButton.HideButton(selectIndex);
        }

        lineList[index].transform.DOKill();
        lineList[index].transform.DOScaleX(1, 0.3f);
        equipmentObjectList[index].SetActive(true);

        characterEquipmentButton.ShowButton(index);

        selectIndex = index;

        if(equipmentObjectList[index].transform.childCount == 0)
            equipmentAttribute.gameObject.SetActive(false);
        else
        {
            equipmentAttribute.gameObject.SetActive(true);
            //equipmentAttribute.Getty
            if(equipmentSystem.CheckEquipEquipment(GetequipmentType(selectIndex)))
            {
                //장착할 장비가 있음
                equipmentList.SelectEquipment(equipmentSystem.GetEquipEquipment(GetequipmentType(index)));
            }
            else
            {
                equipmentList.SelectEquipment(GetequipmentType(index), 0);
            }
        }
        //
        
    }
    public void Clear()
    {
        selectIndex = -1;
    }
    private Type GetequipmentType(int index)
    {
        switch(index)
        {
            case 3:
                return typeof(EquipmentHead);
            case 2:
                return typeof(EquipmentWing);
            case 1:
                return typeof(EquipmentMissle);
            case 0:
                return typeof(EquipmentEngine);
        }
        return null;
    }
}
