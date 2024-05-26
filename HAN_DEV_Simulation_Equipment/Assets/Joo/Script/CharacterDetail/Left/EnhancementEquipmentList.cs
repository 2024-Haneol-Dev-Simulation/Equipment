using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhancementEquipmentList : UIElement
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private EquipmentSystem equipmentSystem;
    [SerializeField] private EquipmentEnhancement equipmentEnhancement;
    [SerializeField] private CharacterEquimentButton characterEquimentButton;
    [SerializeField] private GameObject ElementEuqipmentButtonPrefab;
    [SerializeField] private EnhancementAttribute enhancementAttribute;

    [SerializeField] private GameObject InventoryGameObject;

    [SerializeField] private Button ExitButton;

    private Action _unSelectAction;
    private void Start()
    {
        ExitButton.onClick.AddListener(() =>
        {
            if (enhancementAttribute.gameObject.activeSelf)
                enhancementAttribute.Hide(HideType.Fade);
            else
                Hide(HideType.Fade);
        });
    }
    public void Init()
    {
        List<MyEquipment> myEquipment = playerData.GetAllInventoryEquipment();
        for (int i = 0; i < InventoryGameObject.transform.childCount; i++)
        {
            Destroy(InventoryGameObject.transform.GetChild(i).gameObject);
        }
        foreach (MyEquipment equipment in myEquipment)
        {
            if(equipment.UID == playerData.HeadEquipEquipmentInstanceID ||
                equipment.UID == playerData.WingEquipEquipmentInstanceID ||
                equipment.UID == playerData.MissleEquipEquipmentInstanceID ||
                equipment.UID == playerData.EngineEquipEquipmentInstanceID ||
                equipment.UID == equipmentSystem.SelectEquipmentID)
            { continue; }

            GameObject obj = Instantiate(ElementEuqipmentButtonPrefab, InventoryGameObject.transform);

            //obj.GetComponent<Button>().onClick.RemoveAllListeners();
            if(equipmentEnhancement.ContainEnhancementMaterial(equipment.UID))
            {
                obj.GetComponent<ElementEuqipmentButton>().InitButton(equipment, () =>
                {
                    enhancementAttribute.Show(ShowType.Fade);
                    enhancementAttribute.Init(equipment);

                    obj.GetComponent<ElementEuqipmentButton>().Select();

                    if (_unSelectAction != null)
                        _unSelectAction();

                    _unSelectAction = () =>
                    {
                        obj.GetComponent<ElementEuqipmentButton>().UnSelect();
                        _unSelectAction = null;
                    };
                }, () =>
                {
                    equipmentEnhancement.RemoveEnhancementMaterial(equipment.UID);
                    Element(obj, equipment);
                });
            }
            else
            {
                Element(obj, equipment);
            }
        }
    }
    private void Element(GameObject obj,MyEquipment equipment)
    {
        obj.GetComponent<ElementEuqipmentButton>().InitButton(equipment, () =>
        {
            enhancementAttribute.Show(ShowType.Fade);
            enhancementAttribute.Init(equipment);



            if (_unSelectAction != null)
                _unSelectAction();
            _unSelectAction = () =>
            {
                obj.GetComponent<ElementEuqipmentButton>().UnSelect();
                _unSelectAction = null;
            };
            obj.GetComponent<ElementEuqipmentButton>().Select();

            if (equipmentEnhancement.EnhancementMaterialListCount() >= 15)
                return;
            if (equipmentEnhancement.CheackMax())
                return;

            if (!equipmentEnhancement.ContainEnhancementMaterial(equipment.UID))
            {

                equipmentEnhancement.AddEnhancementMaterial(equipment);
                
            }

            obj.GetComponent<ElementEuqipmentButton>().AddCancelAction(() =>
            {
                equipmentEnhancement.RemoveEnhancementMaterial(equipment.UID);
                Element(obj, equipment);
            });

            //Init();
        });
    }
    public override void Hide(HideType hideType, Complete complete = null)
    {
        characterEquimentButton.ShowButton(equipmentSystem.GetEquipment(equipmentSystem.SelectEquipmentID).equipment.GetType());
        if (hideType == HideType.Fade)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            gameObject.GetComponent<CanvasGroup>().DOFade(0, 0.2f).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }

    public override void Show(ShowType showType, Complete complete = null)
    {
        //Debug.Log(equipmentSystem.GetEquipment(equipmentSystem.SelectEquipmentID).equipment.GetType());
        characterEquimentButton.HideButton(equipmentSystem.GetEquipment(equipmentSystem.SelectEquipmentID).equipment.GetType());
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
