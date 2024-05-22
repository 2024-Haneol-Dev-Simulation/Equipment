using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class EquipmentList : UIElement
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject ElementEuqipmentButtonPrefab;
    [SerializeField] private EquipmentAttribute equipmentAttribute;

    [SerializeField] private GameObject Head_EquipmentInventory;
    [SerializeField] private GameObject Wing_EquipmentInventory;
    [SerializeField] private GameObject Missle_EquipmentInventory;
    [SerializeField] private GameObject Engine_EquipmentInventory;

    private List<GameObject> headEquipmentList;
    private List<GameObject> wingEquipmentList;
    private List<GameObject> missleEquipmentList;
    private List<GameObject> engineEquipmentList;
    public void Init()
    {
        List<MyEquipment> myEquipment = playerData.GetAllInventoryEquipment();

        headEquipmentList = new List<GameObject>();
        wingEquipmentList = new List<GameObject>();
        missleEquipmentList = new List<GameObject>();
        engineEquipmentList = new List<GameObject>();

        DestoryChild(Head_EquipmentInventory);
        DestoryChild(Wing_EquipmentInventory);
        DestoryChild(Missle_EquipmentInventory);
        DestoryChild(Engine_EquipmentInventory);

        foreach (MyEquipment item in myEquipment)
        {
            GameObject obj;
            if (item.equipment.GetType() == typeof(EquipmentHead))
            {
                Debug.Log("Head");
                obj = Instantiate(ElementEuqipmentButtonPrefab, Head_EquipmentInventory.transform);
                obj.GetComponent<ElementEuqipmentButton>().InitButton(item, ()=> { equipmentAttribute.Init(item); });
                headEquipmentList.Add(obj);
            }
            else if(item.equipment.GetType() == typeof(EquipmentWing))
            {
                Debug.Log("Wing");
                obj = Instantiate(ElementEuqipmentButtonPrefab, Wing_EquipmentInventory.transform);
                obj.GetComponent<ElementEuqipmentButton>().InitButton(item, () => { equipmentAttribute.Init(item); });
                wingEquipmentList.Add (obj);
            }
            else if (item.equipment.GetType() == typeof(EquipmentMissle))
            {
                Debug.Log("Missle");
                obj = Instantiate(ElementEuqipmentButtonPrefab, Missle_EquipmentInventory.transform);
                obj.GetComponent<ElementEuqipmentButton>().InitButton(item, () => { equipmentAttribute.Init(item); });
                missleEquipmentList.Add(obj);
            }
            else if (item.equipment.GetType() == typeof(EquipmentEngine))
            {
                Debug.Log("Engine");
                obj = Instantiate(ElementEuqipmentButtonPrefab, Engine_EquipmentInventory.transform);
                obj.GetComponent<ElementEuqipmentButton>().InitButton(item, () => { equipmentAttribute.Init(item); });
                engineEquipmentList.Add(obj);
            }
        }
    }
    public void SelectEquipment(MyEquipment myEquipment)
    {
        List<GameObject> targtList;
        if (myEquipment.equipment.GetType() == typeof(EquipmentHead))
        {
            targtList = headEquipmentList;
        }
        else if (myEquipment.equipment.GetType() == typeof(EquipmentWing))
        {
            targtList = wingEquipmentList;
        }
        else if (myEquipment.equipment.GetType() == typeof(EquipmentMissle))
        {
            targtList = missleEquipmentList;
        }
        else
        {
            targtList = engineEquipmentList;
        }

        foreach (GameObject obj in targtList)
        {
            if(obj.GetComponent<ElementEuqipmentButton>().CheackEqualsequipment(myEquipment))
            {
                obj.GetComponent<ElementEuqipmentButton>().Select();
                obj.GetComponent<Button>().onClick.Invoke();
            }
        }
    }
    public void SelectEquipment(Type type,int index)
    {
        List<GameObject> targtList;
        if (type == typeof(EquipmentHead))
        {
            targtList = headEquipmentList;
        }
        else if (type == typeof(EquipmentWing))
        {
            targtList = wingEquipmentList;
        }
        else if (type == typeof(EquipmentMissle))
        {
            targtList = missleEquipmentList;
        }
        else
        {
            targtList = engineEquipmentList;
        }

        targtList[index].GetComponent<ElementEuqipmentButton>().Select();
        targtList[index].GetComponent<Button>().onClick.Invoke();
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
        gameObject.SetActive(false);
    }

    public override void Show(ShowType showType, Complete complete = null)
    {
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
