using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
using static UnityEditor.Progress;
using UnityEditor;

public class EquipmentList : UIElement
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private EquipmentSystem equipmentSystem;
    [SerializeField] private EquipmentListMenu menu;
    [SerializeField] private GameObject ElementEuqipmentButtonPrefab;
    [SerializeField] private EquipmentAttribute equipmentAttribute;
    [SerializeField] protected CharacterEquimentButton characterEquimentButton;

    [SerializeField] private GameObject Head_EquipmentInventory;
    [SerializeField] private GameObject Wing_EquipmentInventory;
    [SerializeField] private GameObject Missle_EquipmentInventory;
    [SerializeField] private GameObject Engine_EquipmentInventory;

    private Dictionary<string,GameObject> equipmentDictionary;

    private Action _unSelectAction;
    public void Init()
    {
        List<MyEquipment> myEquipment = playerData.GetAllInventoryEquipment();

        equipmentDictionary = new Dictionary<string, GameObject>();

        DestoryChild(Head_EquipmentInventory);
        DestoryChild(Wing_EquipmentInventory);
        DestoryChild(Missle_EquipmentInventory);
        DestoryChild(Engine_EquipmentInventory);

        Transform itemParent;
        string EquipEquipmentUid = string.Empty;
        foreach (MyEquipment item in myEquipment)
        {
            GameObject obj;
            if (item.equipment.GetType() == typeof(EquipmentHead))
            {
                itemParent = Head_EquipmentInventory.transform;
                EquipEquipmentUid = playerData.HeadEquipEquipmentInstanceID;
            }
            else if(item.equipment.GetType() == typeof(EquipmentWing))
            {
                itemParent = Wing_EquipmentInventory.transform;
                EquipEquipmentUid = playerData.WingEquipEquipmentInstanceID;
            }
            else if (item.equipment.GetType() == typeof(EquipmentMissle))
            {
                itemParent = Missle_EquipmentInventory.transform;
                EquipEquipmentUid = playerData.MissleEquipEquipmentInstanceID;
            }
            else 
            {
                itemParent = Engine_EquipmentInventory.transform;
                EquipEquipmentUid = playerData.EngineEquipEquipmentInstanceID;
            }
            //-------------------------------------
            obj = Instantiate(ElementEuqipmentButtonPrefab, itemParent);
            obj.GetComponent<ElementEuqipmentButton>().InitButton(item, () => {
                equipmentSystem.SelectEquipmentID = item.UID;
                equipmentAttribute.Init();
                characterEquimentButton.SetButton(item);
                obj.GetComponent<ElementEuqipmentButton>().Select();

                if (_unSelectAction != null)
                    _unSelectAction();


                _unSelectAction = () =>
                {
                    obj.GetComponent<ElementEuqipmentButton>().UnSelect();
                    _unSelectAction = null;
                };
            });

            equipmentDictionary.Add(item.UID, obj);
            if (item.UID == EquipEquipmentUid)
            {
                obj.GetComponent<ElementEuqipmentButton>().Equip();
            }

            if (equipmentSystem.SelectEquipmentID == item.UID)
            {
                obj.GetComponent<Button>().onClick.Invoke();
            }
        }
    }
    public void SelectEquipment(MyEquipment myEquipment)
    {
        equipmentDictionary[myEquipment.UID].GetComponent<ElementEuqipmentButton>().Equip();
        equipmentDictionary[myEquipment.UID].GetComponent<Button>().onClick.Invoke();
    }
    public void SelectEquipment(Type type,int index)
    {
        GameObject targtParent;
        if (type == typeof(EquipmentHead))
        {
            targtParent = Head_EquipmentInventory;
            //menu.OpenEquipmentList(0);
        }
        else if (type == typeof(EquipmentWing))
        {
            targtParent = Wing_EquipmentInventory;
            //menu.OpenEquipmentList(1);
        }
        else if (type == typeof(EquipmentMissle))
        {
            targtParent = Missle_EquipmentInventory;
            //menu.OpenEquipmentList(2);
        }
        else
        {
            targtParent = Engine_EquipmentInventory;
            //menu.OpenEquipmentList(3);
        }
        
        targtParent.transform.GetChild(index).GetComponent<ElementEuqipmentButton>().Select();
        targtParent.transform.GetChild(index).GetComponent<Button>().onClick.Invoke();
    }
    public GameObject GetButton(string uid)
    {
        if (!equipmentDictionary.ContainsKey(uid))
        {
            Debug.LogError($"equipmentDictionary not ContainsKey : {uid}");
            return null;
        }
        return equipmentDictionary[uid];
    }
    private void DestoryChild(GameObject obj)
    {
        for(int i = 0; i<obj.transform.childCount; i++)
        {
            Destroy(obj.transform.GetChild(i).gameObject);
        }
    }
    public override void Hide(HideType hideType, Complete complete = null)
    {
        //equipmentSystem.SelectEquipmentID = string.Empty;
        gameObject.SetActive(false);
    }

    public override void Show(ShowType showType, Complete complete = null)
    {
        _unSelectAction = null;
        Init();
        gameObject.SetActive(true);
        if (showType == ShowType.FadeAndMove)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 55.45f, 0);
            gameObject.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
            gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector3(-14, 55.45f, 0), 0.3f);
        }
        else if (showType == ShowType.Fade)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        }
    }
}
