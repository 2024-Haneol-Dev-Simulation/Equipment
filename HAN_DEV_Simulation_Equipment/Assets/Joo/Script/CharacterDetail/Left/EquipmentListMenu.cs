using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
        selectIndex = -1;

        buttonList[0].onClick.AddListener(() => { OpenEquipmentList(0); });
        buttonList[1].onClick.AddListener(() => { OpenEquipmentList(1); });
        buttonList[2].onClick.AddListener(() => { OpenEquipmentList(2); });
        buttonList[3].onClick.AddListener(() => { OpenEquipmentList(3); });

        AddEventTrigger(buttonList[0].gameObject,0);
        AddEventTrigger(buttonList[1].gameObject, 1);
        AddEventTrigger(buttonList[2].gameObject, 2);
        AddEventTrigger(buttonList[3].gameObject, 3);
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
            UnSelectButton(buttonList[selectIndex].gameObject);
        }

        lineList[index].transform.DOKill();
        lineList[index].transform.DOScaleX(1, 0.3f);
        equipmentObjectList[index].SetActive(true);
        SelectButton(buttonList[index].gameObject);

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
    public void ImmediatelyOpenEquipmentList(int index)
    {
        if (selectIndex != -1)
        {
            lineList[selectIndex].transform.DOKill();
            lineList[selectIndex].transform.localScale = new Vector3(0,1,1);

            equipmentObjectList[selectIndex].SetActive(false);

            //characterEquipmentButton.HideButton(selectIndex);
            equipmentObjectList[selectIndex].transform.GetChild(0).GetComponent<Image>().DOKill();

            buttonList[selectIndex].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
            buttonList[selectIndex].transform.GetChild(1).GetComponent<TMP_Text>().color = new Color(1, 1, 1, 0.5f);
        }

        selectIndex = index;

        equipmentObjectList[index].SetActive(true);
        lineList[index].transform.localScale = Vector3.one;
        SelectButton(buttonList[index].gameObject);

        if (equipmentObjectList[index].transform.childCount == 0)
            equipmentAttribute.gameObject.SetActive(false);
        else
        {
            equipmentAttribute.gameObject.SetActive(true);
            //equipmentAttribute.Getty
            if (equipmentSystem.CheckEquipEquipment(GetequipmentType(selectIndex)))
            {
                //장착할 장비가 있음
                equipmentList.SelectEquipment(equipmentSystem.GetEquipEquipment(GetequipmentType(index)));
            }
            else
            {
                equipmentList.SelectEquipment(GetequipmentType(index), 0);
            }
        }
    }
    public void Clear()
    {
        selectIndex = -1;
    }

    private void SelectButton(GameObject obj)
    {
        obj.transform.GetChild(0).GetComponent<Image>().DOFade(1, 0.3f);

        obj.transform.GetChild(1).GetComponent<TMP_Text>().DOFade(1, 0.3f);
        obj.transform.GetChild(1).GetComponent<TMP_Text>().DOColor(new Color32(74, 85,99,255), 0.3f);
    }
    private void UnSelectButton(GameObject obj)
    {
        obj.transform.GetChild(0).GetComponent<Image>().DOFade(0, 0.3f);

        obj.transform.GetChild(1).GetComponent<TMP_Text>().DOColor(new Color(1,1,1,0.5f), 0.3f);
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

    private void AddEventTrigger(GameObject obj,int index)
    {
        EventTrigger.Entry entry_PointerEnter = new EventTrigger.Entry();
        EventTrigger.Entry entry_PointerExit = new EventTrigger.Entry();

        entry_PointerEnter.eventID = EventTriggerType.PointerEnter;
        entry_PointerExit.eventID = EventTriggerType.PointerExit;



        entry_PointerEnter.callback.AddListener((data) =>
        {
            if (selectIndex == index)
                return;
            obj.transform.GetChild(1).GetComponent<TMP_Text>().DOKill();
            obj.transform.GetChild(1).GetComponent<TMP_Text>().DOFade(1f, 0.3f);
        });
        entry_PointerExit.callback.AddListener((data) =>
        {
            if (selectIndex == index)
                return;
            obj.transform.GetChild(1).GetComponent<TMP_Text>().DOKill();
            obj.transform.GetChild(1).GetComponent<TMP_Text>().DOFade(0.5f, 0.3f);
        });



        obj.GetComponent<EventTrigger>().triggers.Add(entry_PointerEnter);
        obj.GetComponent<EventTrigger>().triggers.Add(entry_PointerExit);
    }
}
